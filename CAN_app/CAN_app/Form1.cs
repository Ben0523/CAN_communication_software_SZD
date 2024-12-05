using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Collections;
using System.Xml.Schema;
using System.Threading;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using FormsTextBox = System.Windows.Forms.TextBox;

namespace CAN_app
{
    
    public partial class Form1 : Form
    {

        Queue<byte[]> q_buffer = new Queue<byte[]>();
        byte sync1 = 181;
        byte sync2 = 98;
        byte ubx_id = 0;
        bool good_canid, mess_ok,open = false;
        int cnt_frame,data_length= 0;
        int rx_length;
        int mode = 0;
        DateTime now_rx;
        DateTime now_tx;
        

        public byte[] CAN_data_feltoltes(int dlc)
        {
            byte[] can_data = new byte[dlc];
            for (int i = 0; i < dlc; i++)
            {
                string textBoxName = "tb_tx_can_data" + i;

                FormsTextBox tb = this.Controls.Find(textBoxName, true)[0] as FormsTextBox;

                if (tb != null)
                {
                    can_data[i] = Convert.ToByte(tb.Text);
                }
            }
            return can_data;
        }

        public byte[] CalculateChecksum(byte Id, byte[] length ,byte[] payload)
        {
            byte ckA = 0;
            byte ckB = 0;
            byte[] ck_buffer = new byte[1+length.Length +payload.Length];
            ck_buffer[0] = Id;
            Buffer.BlockCopy(length, 0, ck_buffer, 1, length.Length);
            Buffer.BlockCopy(payload, 0, ck_buffer, 1 + length.Length, payload.Length);

            for (int i = 0; i < ck_buffer.Length; i++)
            {
                ckA = (byte)(ckA + ck_buffer[i]);
                ckB = (byte)(ckB + ckA);
            }

            return new byte[] { ckA, ckB };
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(() => Kiiratas());
            timer1.Start();
            string[] ports = SerialPort.GetPortNames();
            cmb_ports.Items.AddRange(ports);
            try
            {
                cmb_ports.SelectedIndex = 0;
            }
            catch
            {
                //MessageBox.Show("Nincs kiválasztható port!");
            }
            cmb_modes.SelectedIndex = 0;
            cmb_DLC.SelectedIndex = 0;
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            
            if (serialPort1.IsOpen)
            {
                //Hiba kezeles//
                if (mode == 0)
                {
                    if (int.Parse(tb_tx_can_id.Text) > 2047)
                    {
                        good_canid = false;
                    }
                    else
                    {
                        good_canid = true;
                    }
                }
                if (mode == 1)
                {
                    if (Int64.Parse(tb_tx_can_id.Text) > 536870911)
                    {
                        good_canid = false;
                    }
                    else
                    {
                        good_canid = true;
                    }
                }

                //Kuldes//
                if (good_canid)
                {
                    ubx_id = Convert.ToByte(lbl_id_minta.Text);
                    byte[] temp_can_id = BitConverter.GetBytes(int.Parse(tb_tx_can_id.Text));
                    Array.Reverse(temp_can_id);
                    int can_id_hossz =0;
                    if (mode == 0)
                    {
                        //Normal
                        can_id_hossz = 2;
                    }
                    else if (mode == 1)
                    {
                        //Extended
                        can_id_hossz = 4;
                    }
                    byte[] can_id = new byte[can_id_hossz];
                    Buffer.BlockCopy(temp_can_id, 4-can_id_hossz, can_id, 0, can_id_hossz);

                    byte[] temp_can_data = CAN_data_feltoltes(int.Parse(cmb_DLC.Text));

                    byte[] payload = new byte[can_id.Length+temp_can_data.Length];
                    for (int i = 0; i < can_id_hossz; i++)
                    {
                        payload[i] = can_id[i];
                    }
                    for (int i = 0; i < temp_can_data.Length; i++)
                    {
                        payload[can_id_hossz+i] = temp_can_data[i];
                    }

                    byte[] length = new byte[2];
                    ushort hossz = Convert.ToByte(can_id_hossz + int.Parse(cmb_DLC.Text));
                    length[0] = (byte)((hossz >> 8) & 0xFF);
                    length[1] = (byte)(hossz & 0xFF);
                    byte[] checksum = CalculateChecksum(ubx_id, length, payload);
                    byte[] ubxFrame = new byte[3 + length.Length + payload.Length + checksum.Length];
                    ubxFrame[0] = sync1;
                    ubxFrame[1] = sync2;
                    ubxFrame[2] = ubx_id;
                    Buffer.BlockCopy(length, 0, ubxFrame, 3, length.Length);
                    Buffer.BlockCopy(payload, 0, ubxFrame, 3 + length.Length, payload.Length);
                    Buffer.BlockCopy(checksum, 0, ubxFrame, 3 + length.Length + payload.Length, checksum.Length);

                    now_tx = DateTime.Now;
                    textBox_dataout.Text += now_tx.ToLongTimeString()+" { ";
                    for (int i = 0; i < ubxFrame.Length; i++)
                    {
                        textBox_dataout.Text += Convert.ToString(ubxFrame[i])+" ";
                    }
                    textBox_dataout.Text += " }" + Environment.NewLine;

                    serialPort1.Write(ubxFrame, 0, ubxFrame.Length);
                }
            }
            else
            {
                MessageBox.Show("Nincs nyitva a Port!");
            }
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            try
            {
                //UART INIT//
                serialPort1.PortName = cmb_ports.Text;
                serialPort1.BaudRate = Convert.ToInt32("9600");
                serialPort1.DataBits = Convert.ToInt32("8");
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
                serialPort1.Open();

                if (!open)
                {
                    btn_open.BackColor = Color.Green;
                    btn_close.BackColor = Color.White;
                    open = true;
                }
            }
            catch
            {
                MessageBox.Show("Nincs port kiválasztva!");
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Bufferes kiiratas//
            Thread.Sleep(50);
            cnt_frame++;
            data_length = serialPort1.BytesToRead;
            byte[] rx_buffer = new byte[data_length+3];
            serialPort1.Read(rx_buffer, 0, data_length);
            now_rx = DateTime.Now;
            rx_buffer[rx_buffer.Length - 1] = (byte)now_rx.Hour;
            rx_buffer[rx_buffer.Length - 2] = (byte)now_rx.Minute;
            rx_buffer[rx_buffer.Length - 3] = (byte)now_rx.Second;
            lock (q_buffer)
            {
                q_buffer.Enqueue(rx_buffer);
            }

        }

        public void Kiiratas()
        {
            while (true)
            {
                lock (q_buffer)
                {
                    if (q_buffer.Count > 0)
                    {

                        //Feldolgozas//
                        byte[] temp_buffer = q_buffer.Dequeue();

                        try
                        {
                            //Rendes program
                            rx_length = temp_buffer[3] + temp_buffer[4];
                            string temp2_length = string.Empty;
                            for (int i = 0; i < 2; i++)
                            {
                                temp2_length += temp_buffer[3 + i].ToString() + " ";
                            }
                            string temp2_payload = string.Empty;
                            for (int i = 0; i < rx_length; i++)
                            {
                                temp2_payload += temp_buffer[5 + i].ToString() + " ";
                            }
                            string temp2_ck = temp_buffer[5 + rx_length].ToString() + " " + temp_buffer[6 + rx_length].ToString() + " ";
                            string rx_data = " { " + temp_buffer[0].ToString() + " " + temp_buffer[1].ToString() + " " + temp_buffer[2].ToString() + " " + temp2_length + temp2_payload + temp2_ck + " }";


                            //Ellenorzes//
                            byte[] len_ck = { temp_buffer[3], temp_buffer[4] };
                            byte[] pl_ck = new byte[rx_length];
                            for (int i = 0; i < rx_length; i++)
                            {
                                pl_ck[i] = temp_buffer[5 + i];
                            }
                            byte[] rx_ck = CalculateChecksum(temp_buffer[2], len_ck, pl_ck);
                            if (temp_buffer[5 + rx_length] == rx_ck[0] && temp_buffer[6 + rx_length] == rx_ck[1])
                            {
                                mess_ok = true;
                            }
                            else
                            {
                                mess_ok = false;
                            }
                            string idobelyeg = temp_buffer[temp_buffer.Length - 1].ToString() + ":" + temp_buffer[temp_buffer.Length - 2].ToString() + ":" + temp_buffer[temp_buffer.Length - 3].ToString();
                            
                            //Kiíratás//
                            this.Invoke(new Action(() =>
                            {
                                textBox_datain.Text += idobelyeg + rx_data + Environment.NewLine;
                            }));

                        }
                        catch
                        {
                            this.Invoke(new Action(() =>
                            {
                                for (int i = 0; i < temp_buffer.Length - 3; i++)
                                {
                                    textBox_datain.Text += temp_buffer[i].ToString() + " ";
                                }
                                mess_ok =false;
                            }));
                        }
                    }
                }
                Thread.Sleep(500);
            }
        }

        private void tb_tx_can_id_TextChanged(object sender, EventArgs e)
        {
            string newText = "";
            foreach (char c in tb_tx_can_id.Text)
            {
                if (char.IsDigit(c))
                {
                    newText += c;
                }
            }
            tb_tx_can_id.Text = newText;
            if (string.IsNullOrEmpty(tb_tx_can_id.Text))
            {
                tb_tx_can_id.Text = "1";
                tb_tx_can_id.SelectionStart = tb_tx_can_id.Text.Length;
            }
            if (mode == 0)
            {
                if (int.Parse(tb_tx_can_id.Text) > 2047)
                {
                    tb_tx_can_id.ForeColor = Color.Red;
                }
                else
                {
                    tb_tx_can_id.ForeColor = Color.Black;
                }
            }
            if (mode == 1)
            {
                if (Int64.Parse(tb_tx_can_id.Text) > 536870911)
                {
                    tb_tx_can_id.ForeColor = Color.Red;
                }
                else
                {
                    tb_tx_can_id.ForeColor = Color.Black;
                }
            }

        }

        private void cmb_DLC_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                string textBoxName = "tb_tx_can_data" +i;

                FormsTextBox tb = this.Controls.Find(textBoxName, true)[0] as FormsTextBox;

                tb.Enabled = false;

            }
            for (int i = 0; i < int.Parse(cmb_DLC.Text); i++)
            {
                string textBoxName = "tb_tx_can_data" + i;

                FormsTextBox tb = this.Controls.Find(textBoxName, true)[0] as FormsTextBox;

                tb.Enabled = true;
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_frame_cnt.Text = $"Frame count: {cnt_frame}";
            lbl_buffer_length.Text = $"Buffer length: {q_buffer.Count.ToString()}";
            if (mess_ok)
            {
                lbl_mess_ok.Text = "Message: OK";
                lbl_mess_ok.ForeColor = Color.Green;
            }
            else if(cnt_frame > 0)
            {
                lbl_mess_ok.Text = "Message: Not OK";
                lbl_mess_ok.ForeColor = Color.Red;
            }

        }

        private void tb_tx_can_data0_TextChanged(object sender, EventArgs e)
        {
            string newText = "";
            foreach (char c in tb_tx_can_data0.Text)
            {
                if (char.IsDigit(c))
                {
                    newText += c;
                }
            }
            tb_tx_can_data0.Text = newText;
            if (string.IsNullOrEmpty(tb_tx_can_data0.Text))
            {
                tb_tx_can_data0.Text = "0";
                tb_tx_can_data0.SelectionStart = tb_tx_can_data0.Text.Length;
            }
            if (int.Parse(tb_tx_can_data0.Text) > byte.MaxValue)
            {
                tb_tx_can_data0.Text = "255";
                tb_tx_can_data0.SelectionStart = tb_tx_can_data0.Text.Length;
            }
        }

        private void tb_tx_can_data1_TextChanged(object sender, EventArgs e)
        {
            string newText = "";
            foreach (char c in tb_tx_can_data1.Text)
            {
                if (char.IsDigit(c))
                {
                    newText += c;
                }
            }
            tb_tx_can_data1.Text = newText;
            if (string.IsNullOrEmpty(tb_tx_can_data1.Text))
            {
                tb_tx_can_data1.Text = "0";
                tb_tx_can_data1.SelectionStart = tb_tx_can_data1.Text.Length;
            }
            if (int.Parse(tb_tx_can_data1.Text) > byte.MaxValue)
            {
                tb_tx_can_data1.Text = "255";
                tb_tx_can_data1.SelectionStart = tb_tx_can_data1.Text.Length;
            }
        }

        private void tb_tx_can_data2_TextChanged(object sender, EventArgs e)
        {
            string newText = "";
            foreach (char c in tb_tx_can_data2.Text)
            {
                if (char.IsDigit(c))
                {
                    newText += c;
                }
            }
            tb_tx_can_data2.Text = newText;
            if (string.IsNullOrEmpty(tb_tx_can_data2.Text))
            {
                tb_tx_can_data2.Text = "0";
                tb_tx_can_data2.SelectionStart = tb_tx_can_data2.Text.Length;
            }
            if (int.Parse(tb_tx_can_data2.Text) > byte.MaxValue)
            {
                tb_tx_can_data2.Text = "255";
                tb_tx_can_data2.SelectionStart = tb_tx_can_data2.Text.Length;
            }
        }

        private void tb_tx_can_data3_TextChanged(object sender, EventArgs e)
        {
            string newText = "";
            foreach (char c in tb_tx_can_data3.Text)
            {
                if (char.IsDigit(c))
                {
                    newText += c;
                }
            }
            tb_tx_can_data3.Text = newText;
            if (string.IsNullOrEmpty(tb_tx_can_data3.Text))
            {
                tb_tx_can_data3.Text = "0";
                tb_tx_can_data3.SelectionStart = tb_tx_can_data3.Text.Length;
            }
            if (int.Parse(tb_tx_can_data3.Text) > byte.MaxValue)
            {
                tb_tx_can_data3.Text = "255";
                tb_tx_can_data3.SelectionStart = tb_tx_can_data3.Text.Length;
            }
        }

        private void tb_tx_can_data4_TextChanged(object sender, EventArgs e)
        {
            string newText = "";
            foreach (char c in tb_tx_can_data4.Text)
            {
                if (char.IsDigit(c))
                {
                    newText += c;
                }
            }
            tb_tx_can_data4.Text = newText;
            if (string.IsNullOrEmpty(tb_tx_can_data4.Text))
            {
                tb_tx_can_data4.Text = "0";
                tb_tx_can_data4.SelectionStart = tb_tx_can_data4.Text.Length;
            }
            if (int.Parse(tb_tx_can_data4.Text) > byte.MaxValue)
            {
                tb_tx_can_data4.Text = "255";
                tb_tx_can_data4.SelectionStart = tb_tx_can_data4.Text.Length;
            }
        }

        private void tb_tx_can_data5_TextChanged(object sender, EventArgs e)
        {
            string newText = "";
            foreach (char c in tb_tx_can_data5.Text)
            {
                if (char.IsDigit(c))
                {
                    newText += c;
                }
            }
            tb_tx_can_data5.Text = newText;
            if (string.IsNullOrEmpty(tb_tx_can_data5.Text))
            {
                tb_tx_can_data5.Text = "0";
                tb_tx_can_data5.SelectionStart = tb_tx_can_data5.Text.Length;
            }
            if (int.Parse(tb_tx_can_data5.Text) > byte.MaxValue)
            {
                tb_tx_can_data5.Text = "255";
                tb_tx_can_data5.SelectionStart = tb_tx_can_data5.Text.Length;
            }
        }

        private void tb_tx_can_data6_TextChanged(object sender, EventArgs e)
        {
            string newText = "";
            foreach (char c in tb_tx_can_data6.Text)
            {
                if (char.IsDigit(c))
                {
                    newText += c;
                }
            }
            tb_tx_can_data6.Text = newText;
            if (string.IsNullOrEmpty(tb_tx_can_data6.Text))
            {
                tb_tx_can_data6.Text = "0";
                tb_tx_can_data6.SelectionStart = tb_tx_can_data6.Text.Length;
            }
            if (int.Parse(tb_tx_can_data6.Text) > byte.MaxValue)
            {
                tb_tx_can_data6.Text = "255";
                tb_tx_can_data6.SelectionStart = tb_tx_can_data6.Text.Length;
            }
        }

        private void tb_tx_can_data7_TextChanged(object sender, EventArgs e)
        {
            string newText = "";
            foreach (char c in tb_tx_can_data7.Text)
            {
                if (char.IsDigit(c))
                {
                    newText += c;
                }
            }
            tb_tx_can_data7.Text = newText;
            if (string.IsNullOrEmpty(tb_tx_can_data7.Text))
            {
                tb_tx_can_data7.Text = "0";
                tb_tx_can_data7.SelectionStart = tb_tx_can_data7.Text.Length;
            }
            if (int.Parse(tb_tx_can_data7.Text) > byte.MaxValue)
            {
                tb_tx_can_data7.Text = "255";
                tb_tx_can_data7.SelectionStart = tb_tx_can_data7.Text.Length;
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            textBox_datain.Text = "";
            textBox_dataout.Text = "";
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            if (open)
            {
                btn_open.BackColor = Color.White;
                btn_close.BackColor = Color.Red;
                open = false;
            }
            serialPort1.Close();
        }

        private void cmb_modes_SelectedIndexChanged(object sender, EventArgs e)
        {
            mode = cmb_modes.SelectedIndex;
            if (mode == 0)
            {
                lbl_id_minta.Text = "1";
            }
            else if (mode == 1)
            {
                lbl_id_minta.Text = "3";
            }
            tb_tx_can_id_TextChanged(sender, e);
        }
    }
}
