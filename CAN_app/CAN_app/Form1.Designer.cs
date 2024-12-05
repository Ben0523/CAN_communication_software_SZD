namespace CAN_app
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.cmb_ports = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_dataout = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.btn_open = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_datain = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_clear = new System.Windows.Forms.Button();
            this.label_id = new System.Windows.Forms.Label();
            this.Tx_groupbox = new System.Windows.Forms.GroupBox();
            this.lbl_mode = new System.Windows.Forms.Label();
            this.cmb_modes = new System.Windows.Forms.ComboBox();
            this.lbl_id_minta = new System.Windows.Forms.Label();
            this.Pl_groupbox = new System.Windows.Forms.GroupBox();
            this.cmb_DLC = new System.Windows.Forms.ComboBox();
            this.tb_tx_can_data7 = new System.Windows.Forms.TextBox();
            this.tb_tx_can_data6 = new System.Windows.Forms.TextBox();
            this.tb_tx_can_data5 = new System.Windows.Forms.TextBox();
            this.tb_tx_can_data4 = new System.Windows.Forms.TextBox();
            this.tb_tx_can_data3 = new System.Windows.Forms.TextBox();
            this.tb_tx_can_data2 = new System.Windows.Forms.TextBox();
            this.tb_tx_can_data1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_tx_can_data0 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_tx_can_id = new System.Windows.Forms.TextBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.lbl_frame_cnt = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbl_buffer_length = new System.Windows.Forms.Label();
            this.lbl_mess_ok = new System.Windows.Forms.Label();
            this.Tx_groupbox.SuspendLayout();
            this.Pl_groupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // cmb_ports
            // 
            this.cmb_ports.FormattingEnabled = true;
            this.cmb_ports.Location = new System.Drawing.Point(86, 27);
            this.cmb_ports.Name = "cmb_ports";
            this.cmb_ports.Size = new System.Drawing.Size(121, 21);
            this.cmb_ports.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(42, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port:";
            // 
            // textBox_dataout
            // 
            this.textBox_dataout.Location = new System.Drawing.Point(138, 71);
            this.textBox_dataout.Multiline = true;
            this.textBox_dataout.Name = "textBox_dataout";
            this.textBox_dataout.Size = new System.Drawing.Size(420, 120);
            this.textBox_dataout.TabIndex = 2;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(45, 145);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(87, 46);
            this.btn_send.TabIndex = 3;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(236, 27);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(75, 24);
            this.btn_open.TabIndex = 4;
            this.btn_open.Text = "Open";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tx data:";
            // 
            // textBox_datain
            // 
            this.textBox_datain.Location = new System.Drawing.Point(677, 71);
            this.textBox_datain.Multiline = true;
            this.textBox_datain.Name = "textBox_datain";
            this.textBox_datain.Size = new System.Drawing.Size(420, 120);
            this.textBox_datain.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(624, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Rx data:";
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(564, 145);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(107, 46);
            this.btn_clear.TabIndex = 8;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // label_id
            // 
            this.label_id.AutoSize = true;
            this.label_id.Location = new System.Drawing.Point(132, 54);
            this.label_id.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_id.Name = "label_id";
            this.label_id.Size = new System.Drawing.Size(46, 13);
            this.label_id.TabIndex = 9;
            this.label_id.Text = "UBX ID:";
            // 
            // Tx_groupbox
            // 
            this.Tx_groupbox.Controls.Add(this.lbl_mode);
            this.Tx_groupbox.Controls.Add(this.cmb_modes);
            this.Tx_groupbox.Controls.Add(this.lbl_id_minta);
            this.Tx_groupbox.Controls.Add(this.Pl_groupbox);
            this.Tx_groupbox.Controls.Add(this.label_id);
            this.Tx_groupbox.Location = new System.Drawing.Point(45, 221);
            this.Tx_groupbox.Margin = new System.Windows.Forms.Padding(2);
            this.Tx_groupbox.Name = "Tx_groupbox";
            this.Tx_groupbox.Padding = new System.Windows.Forms.Padding(2);
            this.Tx_groupbox.Size = new System.Drawing.Size(613, 115);
            this.Tx_groupbox.TabIndex = 10;
            this.Tx_groupbox.TabStop = false;
            this.Tx_groupbox.Text = "Tx:";
            // 
            // lbl_mode
            // 
            this.lbl_mode.AutoSize = true;
            this.lbl_mode.Location = new System.Drawing.Point(6, 36);
            this.lbl_mode.Name = "lbl_mode";
            this.lbl_mode.Size = new System.Drawing.Size(37, 13);
            this.lbl_mode.TabIndex = 17;
            this.lbl_mode.Text = "Mode:";
            // 
            // cmb_modes
            // 
            this.cmb_modes.FormattingEnabled = true;
            this.cmb_modes.Items.AddRange(new object[] {
            "Normal",
            "Extended"});
            this.cmb_modes.Location = new System.Drawing.Point(6, 51);
            this.cmb_modes.Name = "cmb_modes";
            this.cmb_modes.Size = new System.Drawing.Size(121, 21);
            this.cmb_modes.TabIndex = 16;
            this.cmb_modes.SelectedIndexChanged += new System.EventHandler(this.cmb_modes_SelectedIndexChanged);
            // 
            // lbl_id_minta
            // 
            this.lbl_id_minta.AutoSize = true;
            this.lbl_id_minta.Location = new System.Drawing.Point(173, 54);
            this.lbl_id_minta.Name = "lbl_id_minta";
            this.lbl_id_minta.Size = new System.Drawing.Size(13, 13);
            this.lbl_id_minta.TabIndex = 15;
            this.lbl_id_minta.Text = "1";
            // 
            // Pl_groupbox
            // 
            this.Pl_groupbox.Controls.Add(this.cmb_DLC);
            this.Pl_groupbox.Controls.Add(this.tb_tx_can_data7);
            this.Pl_groupbox.Controls.Add(this.tb_tx_can_data6);
            this.Pl_groupbox.Controls.Add(this.tb_tx_can_data5);
            this.Pl_groupbox.Controls.Add(this.tb_tx_can_data4);
            this.Pl_groupbox.Controls.Add(this.tb_tx_can_data3);
            this.Pl_groupbox.Controls.Add(this.tb_tx_can_data2);
            this.Pl_groupbox.Controls.Add(this.tb_tx_can_data1);
            this.Pl_groupbox.Controls.Add(this.label6);
            this.Pl_groupbox.Controls.Add(this.tb_tx_can_data0);
            this.Pl_groupbox.Controls.Add(this.label7);
            this.Pl_groupbox.Controls.Add(this.label5);
            this.Pl_groupbox.Controls.Add(this.label4);
            this.Pl_groupbox.Controls.Add(this.tb_tx_can_id);
            this.Pl_groupbox.Location = new System.Drawing.Point(191, 17);
            this.Pl_groupbox.Margin = new System.Windows.Forms.Padding(2);
            this.Pl_groupbox.Name = "Pl_groupbox";
            this.Pl_groupbox.Padding = new System.Windows.Forms.Padding(2);
            this.Pl_groupbox.Size = new System.Drawing.Size(413, 83);
            this.Pl_groupbox.TabIndex = 14;
            this.Pl_groupbox.TabStop = false;
            this.Pl_groupbox.Text = "Payload:";
            // 
            // cmb_DLC
            // 
            this.cmb_DLC.FormattingEnabled = true;
            this.cmb_DLC.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cmb_DLC.Location = new System.Drawing.Point(365, 37);
            this.cmb_DLC.Name = "cmb_DLC";
            this.cmb_DLC.Size = new System.Drawing.Size(40, 21);
            this.cmb_DLC.TabIndex = 27;
            this.cmb_DLC.SelectedIndexChanged += new System.EventHandler(this.cmb_DLC_SelectedIndexChanged);
            // 
            // tb_tx_can_data7
            // 
            this.tb_tx_can_data7.Enabled = false;
            this.tb_tx_can_data7.Location = new System.Drawing.Point(334, 37);
            this.tb_tx_can_data7.Name = "tb_tx_can_data7";
            this.tb_tx_can_data7.Size = new System.Drawing.Size(25, 20);
            this.tb_tx_can_data7.TabIndex = 26;
            this.tb_tx_can_data7.Text = "0";
            this.tb_tx_can_data7.TextChanged += new System.EventHandler(this.tb_tx_can_data7_TextChanged);
            // 
            // tb_tx_can_data6
            // 
            this.tb_tx_can_data6.Enabled = false;
            this.tb_tx_can_data6.Location = new System.Drawing.Point(303, 37);
            this.tb_tx_can_data6.Name = "tb_tx_can_data6";
            this.tb_tx_can_data6.Size = new System.Drawing.Size(25, 20);
            this.tb_tx_can_data6.TabIndex = 25;
            this.tb_tx_can_data6.Text = "0";
            this.tb_tx_can_data6.TextChanged += new System.EventHandler(this.tb_tx_can_data6_TextChanged);
            // 
            // tb_tx_can_data5
            // 
            this.tb_tx_can_data5.Enabled = false;
            this.tb_tx_can_data5.Location = new System.Drawing.Point(272, 37);
            this.tb_tx_can_data5.Name = "tb_tx_can_data5";
            this.tb_tx_can_data5.Size = new System.Drawing.Size(25, 20);
            this.tb_tx_can_data5.TabIndex = 24;
            this.tb_tx_can_data5.Text = "0";
            this.tb_tx_can_data5.TextChanged += new System.EventHandler(this.tb_tx_can_data5_TextChanged);
            // 
            // tb_tx_can_data4
            // 
            this.tb_tx_can_data4.Enabled = false;
            this.tb_tx_can_data4.Location = new System.Drawing.Point(241, 37);
            this.tb_tx_can_data4.Name = "tb_tx_can_data4";
            this.tb_tx_can_data4.Size = new System.Drawing.Size(25, 20);
            this.tb_tx_can_data4.TabIndex = 23;
            this.tb_tx_can_data4.Text = "0";
            this.tb_tx_can_data4.TextChanged += new System.EventHandler(this.tb_tx_can_data4_TextChanged);
            // 
            // tb_tx_can_data3
            // 
            this.tb_tx_can_data3.Enabled = false;
            this.tb_tx_can_data3.Location = new System.Drawing.Point(210, 37);
            this.tb_tx_can_data3.Name = "tb_tx_can_data3";
            this.tb_tx_can_data3.Size = new System.Drawing.Size(25, 20);
            this.tb_tx_can_data3.TabIndex = 22;
            this.tb_tx_can_data3.Text = "0";
            this.tb_tx_can_data3.TextChanged += new System.EventHandler(this.tb_tx_can_data3_TextChanged);
            // 
            // tb_tx_can_data2
            // 
            this.tb_tx_can_data2.Enabled = false;
            this.tb_tx_can_data2.Location = new System.Drawing.Point(179, 37);
            this.tb_tx_can_data2.Name = "tb_tx_can_data2";
            this.tb_tx_can_data2.Size = new System.Drawing.Size(25, 20);
            this.tb_tx_can_data2.TabIndex = 21;
            this.tb_tx_can_data2.Text = "0";
            this.tb_tx_can_data2.TextChanged += new System.EventHandler(this.tb_tx_can_data2_TextChanged);
            // 
            // tb_tx_can_data1
            // 
            this.tb_tx_can_data1.Enabled = false;
            this.tb_tx_can_data1.Location = new System.Drawing.Point(148, 37);
            this.tb_tx_can_data1.Name = "tb_tx_can_data1";
            this.tb_tx_can_data1.Size = new System.Drawing.Size(25, 20);
            this.tb_tx_can_data1.TabIndex = 20;
            this.tb_tx_can_data1.Text = "0";
            this.tb_tx_can_data1.TextChanged += new System.EventHandler(this.tb_tx_can_data1_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(84, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 19;
            // 
            // tb_tx_can_data0
            // 
            this.tb_tx_can_data0.Enabled = false;
            this.tb_tx_can_data0.Location = new System.Drawing.Point(118, 37);
            this.tb_tx_can_data0.Margin = new System.Windows.Forms.Padding(2);
            this.tb_tx_can_data0.Name = "tb_tx_can_data0";
            this.tb_tx_can_data0.Size = new System.Drawing.Size(25, 20);
            this.tb_tx_can_data0.TabIndex = 17;
            this.tb_tx_can_data0.Text = "0";
            this.tb_tx_can_data0.TextChanged += new System.EventHandler(this.tb_tx_can_data0_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(362, 20);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "DLC:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "CAN Data:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "CAN ID:";
            // 
            // tb_tx_can_id
            // 
            this.tb_tx_can_id.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tb_tx_can_id.Location = new System.Drawing.Point(7, 36);
            this.tb_tx_can_id.Margin = new System.Windows.Forms.Padding(2);
            this.tb_tx_can_id.Name = "tb_tx_can_id";
            this.tb_tx_can_id.Size = new System.Drawing.Size(107, 20);
            this.tb_tx_can_id.TabIndex = 14;
            this.tb_tx_can_id.Text = "1";
            this.tb_tx_can_id.TextChanged += new System.EventHandler(this.tb_tx_can_id_TextChanged);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(320, 27);
            this.btn_close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 24);
            this.btn_close.TabIndex = 11;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // lbl_frame_cnt
            // 
            this.lbl_frame_cnt.AutoSize = true;
            this.lbl_frame_cnt.Location = new System.Drawing.Point(677, 203);
            this.lbl_frame_cnt.Name = "lbl_frame_cnt";
            this.lbl_frame_cnt.Size = new System.Drawing.Size(69, 13);
            this.lbl_frame_cnt.TabIndex = 12;
            this.lbl_frame_cnt.Text = "Frame count:";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbl_buffer_length
            // 
            this.lbl_buffer_length.AutoSize = true;
            this.lbl_buffer_length.Location = new System.Drawing.Point(783, 203);
            this.lbl_buffer_length.Name = "lbl_buffer_length";
            this.lbl_buffer_length.Size = new System.Drawing.Size(70, 13);
            this.lbl_buffer_length.TabIndex = 13;
            this.lbl_buffer_length.Text = "Buffer length:";
            // 
            // lbl_mess_ok
            // 
            this.lbl_mess_ok.AutoSize = true;
            this.lbl_mess_ok.Location = new System.Drawing.Point(991, 203);
            this.lbl_mess_ok.Name = "lbl_mess_ok";
            this.lbl_mess_ok.Size = new System.Drawing.Size(56, 13);
            this.lbl_mess_ok.TabIndex = 14;
            this.lbl_mess_ok.Text = "Message: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 461);
            this.Controls.Add(this.lbl_mess_ok);
            this.Controls.Add(this.lbl_buffer_length);
            this.Controls.Add(this.lbl_frame_cnt);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.Tx_groupbox);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_datain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_open);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.textBox_dataout);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_ports);
            this.Name = "Form1";
            this.Text = "Can_app";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Tx_groupbox.ResumeLayout(false);
            this.Tx_groupbox.PerformLayout();
            this.Pl_groupbox.ResumeLayout(false);
            this.Pl_groupbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox cmb_ports;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_dataout;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_datain;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Label label_id;
        private System.Windows.Forms.GroupBox Tx_groupbox;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.TextBox tb_tx_can_id;
        private System.Windows.Forms.TextBox tb_tx_can_data0;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox Pl_groupbox;
        private System.Windows.Forms.Label lbl_id_minta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_modes;
        private System.Windows.Forms.Label lbl_mode;
        private System.Windows.Forms.Label lbl_frame_cnt;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbl_buffer_length;
        private System.Windows.Forms.Label lbl_mess_ok;
        private System.Windows.Forms.TextBox tb_tx_can_data1;
        private System.Windows.Forms.ComboBox cmb_DLC;
        private System.Windows.Forms.TextBox tb_tx_can_data7;
        private System.Windows.Forms.TextBox tb_tx_can_data6;
        private System.Windows.Forms.TextBox tb_tx_can_data5;
        private System.Windows.Forms.TextBox tb_tx_can_data4;
        private System.Windows.Forms.TextBox tb_tx_can_data3;
        private System.Windows.Forms.TextBox tb_tx_can_data2;
        private System.Windows.Forms.Label label7;
    }
}

