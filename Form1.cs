using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace allping
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource _cancellationTokenSource;

        public Form1()
        {
            InitializeComponent();
            // 添加TextChanged事件处理
            importIPtextBox1.TextChanged += ImportIPtextBox1_TextChanged;
        }

        private void ImportIPtextBox1_TextChanged(object sender, EventArgs e)
        {
            string text = importIPtextBox1.Text;
            
            // 使用正则表达式匹配所有的URL模式
            string pattern = @"(https?://[\d\.:]+)";
            string[] urls = Regex.Split(text, pattern)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.Trim())
                .ToArray();

            // 重新组合文本，确保每个URL独占一行
            string processedText = string.Join(Environment.NewLine, 
                urls.Where(s => s.StartsWith("http://") || s.StartsWith("https://")));

            // 如果处理后的文本与当前文本不同，则更新文本框
            if (processedText != text)
            {
                int selectionStart = importIPtextBox1.SelectionStart;
                importIPtextBox1.Text = processedText;
                if (selectionStart <= processedText.Length)
                {
                    importIPtextBox1.SelectionStart = selectionStart;
                }
            }
        }

        private async void Allping_Click(object sender, EventArgs e)
        {
            if (_cancellationTokenSource != null)
            {
                try
                {
                    var cts = _cancellationTokenSource;  // 保存引用
                    _cancellationTokenSource = null;  // 立即清空引用
                    Allping.Text = "批量ping";
                    
                    cts.Cancel();  // 取消操作
                    try
                    {
                        await Task.Delay(100, new CancellationToken());  // 使用新的取消标记
                    }
                    catch { }
                    
                    cts.Dispose();  // 释放资源
                }
                catch (Exception)
                {
                    // 忽略取消过程中的异常
                }
                return;
            }

            if (string.IsNullOrWhiteSpace(importIPtextBox1.Text))
            {
                MessageBox.Show("请先导入IP地址！", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _cancellationTokenSource = new CancellationTokenSource();
                Allping.Text = "停止ping";

                AllpingtextBox1.Clear();
                Pinglog.Clear();

                string[] originalLines = importIPtextBox1.Text.Split(new[] { Environment.NewLine }, 
                    StringSplitOptions.RemoveEmptyEntries);

                Dictionary<string, string> originalToIP = new Dictionary<string, string>();
                
                foreach (string line in originalLines)
                {
                    if (_cancellationTokenSource.Token.IsCancellationRequested)
                        return;

                    string trimmedLine = line.Trim();
                    // 首先尝试提取URL或IP地址部分，去除后面的ping结果
                    string urlOrIp = trimmedLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    
                    var match = Regex.Match(urlOrIp, @"\b(?:\d{1,3}\.){3}\d{1,3}\b");
                    if (match.Success)
                    {
                        originalToIP[trimmedLine] = match.Value;
                    }
                    else
                    {
                        originalToIP[trimmedLine] = urlOrIp;
                    }
                }

                foreach (string originalLine in originalLines)
                {
                    if (_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        return;
                    }

                    string trimmedLine = originalLine.Trim();
                    string urlOrIp = trimmedLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    string ip = originalToIP[trimmedLine];
                    try
                    {
                        long minRoundtripTime = long.MaxValue;
                        bool hasSuccessfulPing = false;
                        string pingStatus = "timeout";
                        int timeoutCount = 0;
                        int lastSuccessfulTtl = 0;

                        Pinglog.AppendText($"正在 Ping {ip} ...\r\n");
                        Pinglog.Update();

                        using (Ping ping = new Ping())
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (_cancellationTokenSource.Token.IsCancellationRequested)
                                {
                                    return;
                                }

                                PingReply reply = await ping.SendPingAsync(ip);
                                if (reply.Status == IPStatus.Success)
                                {
                                    hasSuccessfulPing = true;
                                    minRoundtripTime = Math.Min(minRoundtripTime, reply.RoundtripTime);
                                    lastSuccessfulTtl = reply.Options?.Ttl ?? 0;
                                    Pinglog.AppendText($"来自 {ip} 的回复: 时间={reply.RoundtripTime}ms TTL={lastSuccessfulTtl}\r\n");
                                }
                                else
                                {
                                    timeoutCount++;
                                    Pinglog.AppendText($"来自 {ip} 的回复: {reply.Status}\r\n");
                                }
                                Pinglog.Update();

                                if (i < 2)
                                {
                                    await Task.Delay(1000, _cancellationTokenSource.Token);
                                }
                            }
                        }

                        if (hasSuccessfulPing)
                        {
                            pingStatus = $"{minRoundtripTime}ms TTL={lastSuccessfulTtl}";
                        }
                        else
                        {
                            pingStatus = "timeout";
                        }

                        string result = string.Format("{0," + (-32) + "}{1," + (-15) + "}超时次数:{2}",
                            urlOrIp,
                            pingStatus,
                            timeoutCount).Replace("\t", "    ");
                        AllpingtextBox1.AppendText(result + Environment.NewLine);
                        AllpingtextBox1.ScrollToCaret();

                        Pinglog.AppendText("\r\n");
                        Pinglog.Update();
                    }
                    catch (Exception ex)
                    {
                        string result = $"{urlOrIp}\t\t错误: {ex.Message}\t超时次数:3";
                        AllpingtextBox1.AppendText(result + Environment.NewLine);
                        Pinglog.AppendText($"Ping {ip} 时发生错误: {ex.Message}\r\n\r\n");
                        Pinglog.Update();
                    }
                }

                if (_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    Pinglog.AppendText("操作已取消\r\n");
                    return;
                }
            }
            catch (OperationCanceledException)
            {
                Pinglog.AppendText("操作已取消\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_cancellationTokenSource != null)
                {
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = null;
                }
                Allping.Text = "批量ping";
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AllpingtextBox1.Text))
            {
                MessageBox.Show("没有可保存的内容！", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Application.StartupPath;
                saveFileDialog.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.DefaultExt = "txt";
                
                saveFileDialog.FileName = $"ping结果_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var lines = AllpingtextBox1.Lines;
                        var processedLines = new List<string>();
                        
                        foreach (var line in lines)
                        {
                            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length >= 3)
                            {
                                string url = parts[0];
                                string ms = parts[1];  // 获取ms部分
                                string ttl = parts[2];  // 获取TTL部分
                                string timeout = parts[3];  // 获取超时次数部分
                                
                                // 使用固定宽度和空格数量重新格式化
                                string formattedLine = $"{url.PadRight(32)}{ms.PadRight(10)}{ttl.PadRight(10)}{timeout}";
                                processedLines.Add(formattedLine);
                            }
                            else
                            {
                                processedLines.Add(line);
                            }
                        }

                        File.WriteAllLines(saveFileDialog.FileName, processedLines);
                        MessageBox.Show("保存成功！", "提示", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"保存文件时发生错误: {ex.Message}", "错误", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void importIP_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(openFileDialog.FileName);
                        importIPtextBox1.Text = string.Join(Environment.NewLine, lines);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("读取文件时发生错误: " + ex.Message, "错误", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
