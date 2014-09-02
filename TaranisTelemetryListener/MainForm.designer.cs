namespace TaranisTelemetryListener
{
    partial class MainForm
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
            System.Windows.Forms.Label baudRateLabel;
            System.Windows.Forms.Label dataBitsLabel;
            System.Windows.Forms.Label parityLabel;
            System.Windows.Forms.Label portNameLabel;
            System.Windows.Forms.Label stopBitsLabel;
            this.baudRateComboBox = new System.Windows.Forms.ComboBox();
            this.serialSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataBitsComboBox = new System.Windows.Forms.ComboBox();
            this.parityComboBox = new System.Windows.Forms.ComboBox();
            this.portNameComboBox = new System.Windows.Forms.ComboBox();
            this.stopBitsComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.VFAS_MIN_textBox = new System.Windows.Forms.TextBox();
            this.VFAS_textBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MinCell_textBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CellsSum_textBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.swr_textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.alt_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RSSI_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Batt_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbData = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Cell1_textBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Cell2_textBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Cell3_textBox = new System.Windows.Forms.TextBox();
            baudRateLabel = new System.Windows.Forms.Label();
            dataBitsLabel = new System.Windows.Forms.Label();
            parityLabel = new System.Windows.Forms.Label();
            portNameLabel = new System.Windows.Forms.Label();
            stopBitsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.serialSettingsBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // baudRateLabel
            // 
            baudRateLabel.AutoSize = true;
            baudRateLabel.Location = new System.Drawing.Point(10, 59);
            baudRateLabel.Name = "baudRateLabel";
            baudRateLabel.Size = new System.Drawing.Size(61, 13);
            baudRateLabel.TabIndex = 1;
            baudRateLabel.Text = "Baud Rate:";
            // 
            // dataBitsLabel
            // 
            dataBitsLabel.AutoSize = true;
            dataBitsLabel.Location = new System.Drawing.Point(10, 86);
            dataBitsLabel.Name = "dataBitsLabel";
            dataBitsLabel.Size = new System.Drawing.Size(53, 13);
            dataBitsLabel.TabIndex = 3;
            dataBitsLabel.Text = "Data Bits:";
            // 
            // parityLabel
            // 
            parityLabel.AutoSize = true;
            parityLabel.Location = new System.Drawing.Point(10, 113);
            parityLabel.Name = "parityLabel";
            parityLabel.Size = new System.Drawing.Size(36, 13);
            parityLabel.TabIndex = 5;
            parityLabel.Text = "Parity:";
            // 
            // portNameLabel
            // 
            portNameLabel.AutoSize = true;
            portNameLabel.Location = new System.Drawing.Point(10, 32);
            portNameLabel.Name = "portNameLabel";
            portNameLabel.Size = new System.Drawing.Size(60, 13);
            portNameLabel.TabIndex = 7;
            portNameLabel.Text = "Port Name:";
            // 
            // stopBitsLabel
            // 
            stopBitsLabel.AutoSize = true;
            stopBitsLabel.Location = new System.Drawing.Point(10, 140);
            stopBitsLabel.Name = "stopBitsLabel";
            stopBitsLabel.Size = new System.Drawing.Size(52, 13);
            stopBitsLabel.TabIndex = 9;
            stopBitsLabel.Text = "Stop Bits:";
            // 
            // baudRateComboBox
            // 
            this.baudRateComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.serialSettingsBindingSource, "BaudRate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.baudRateComboBox.FormattingEnabled = true;
            this.baudRateComboBox.Location = new System.Drawing.Point(77, 56);
            this.baudRateComboBox.Name = "baudRateComboBox";
            this.baudRateComboBox.Size = new System.Drawing.Size(121, 21);
            this.baudRateComboBox.TabIndex = 2;
            // 
            // serialSettingsBindingSource
            // 
            this.serialSettingsBindingSource.DataSource = typeof(TaranisTelemetryListener.Serial.SerialSettings);
            // 
            // dataBitsComboBox
            // 
            this.dataBitsComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.serialSettingsBindingSource, "DataBits", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dataBitsComboBox.FormattingEnabled = true;
            this.dataBitsComboBox.Location = new System.Drawing.Point(77, 83);
            this.dataBitsComboBox.Name = "dataBitsComboBox";
            this.dataBitsComboBox.Size = new System.Drawing.Size(121, 21);
            this.dataBitsComboBox.TabIndex = 4;
            // 
            // parityComboBox
            // 
            this.parityComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.serialSettingsBindingSource, "Parity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.parityComboBox.FormattingEnabled = true;
            this.parityComboBox.Location = new System.Drawing.Point(77, 110);
            this.parityComboBox.Name = "parityComboBox";
            this.parityComboBox.Size = new System.Drawing.Size(121, 21);
            this.parityComboBox.TabIndex = 6;
            // 
            // portNameComboBox
            // 
            this.portNameComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.serialSettingsBindingSource, "PortName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.portNameComboBox.FormattingEnabled = true;
            this.portNameComboBox.Location = new System.Drawing.Point(77, 29);
            this.portNameComboBox.Name = "portNameComboBox";
            this.portNameComboBox.Size = new System.Drawing.Size(121, 21);
            this.portNameComboBox.TabIndex = 8;
            // 
            // stopBitsComboBox
            // 
            this.stopBitsComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.serialSettingsBindingSource, "StopBits", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stopBitsComboBox.FormattingEnabled = true;
            this.stopBitsComboBox.Location = new System.Drawing.Point(77, 137);
            this.stopBitsComboBox.Name = "stopBitsComboBox";
            this.stopBitsComboBox.Size = new System.Drawing.Size(121, 21);
            this.stopBitsComboBox.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.baudRateComboBox);
            this.groupBox1.Controls.Add(baudRateLabel);
            this.groupBox1.Controls.Add(this.stopBitsComboBox);
            this.groupBox1.Controls.Add(stopBitsLabel);
            this.groupBox1.Controls.Add(dataBitsLabel);
            this.groupBox1.Controls.Add(this.portNameComboBox);
            this.groupBox1.Controls.Add(this.dataBitsComboBox);
            this.groupBox1.Controls.Add(portNameLabel);
            this.groupBox1.Controls.Add(parityLabel);
            this.groupBox1.Controls.Add(this.parityComboBox);
            this.groupBox1.Location = new System.Drawing.Point(110, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 171);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Port Settings";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(152, 215);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(85, 23);
            this.btnStart.TabIndex = 12;
            this.btnStart.Text = "Start listening";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(242, 215);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(85, 23);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "Stop listening";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Cell3_textBox);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.Cell2_textBox);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.Cell1_textBox);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.VFAS_MIN_textBox);
            this.groupBox2.Controls.Add(this.VFAS_textBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.MinCell_textBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.CellsSum_textBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.swr_textBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.alt_textBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.RSSI_textBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.Batt_textBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(29, 263);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 188);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current Telemetry";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(169, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "ADC1:";
            // 
            // VFAS_MIN_textBox
            // 
            this.VFAS_MIN_textBox.Location = new System.Drawing.Point(213, 43);
            this.VFAS_MIN_textBox.Name = "VFAS_MIN_textBox";
            this.VFAS_MIN_textBox.Size = new System.Drawing.Size(49, 20);
            this.VFAS_MIN_textBox.TabIndex = 14;
            // 
            // VFAS_textBox
            // 
            this.VFAS_textBox.Location = new System.Drawing.Point(213, 17);
            this.VFAS_textBox.Name = "VFAS_textBox";
            this.VFAS_textBox.Size = new System.Drawing.Size(49, 20);
            this.VFAS_textBox.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(125, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "VARIO SPEED:";
            // 
            // MinCell_textBox
            // 
            this.MinCell_textBox.Location = new System.Drawing.Point(67, 156);
            this.MinCell_textBox.Name = "MinCell_textBox";
            this.MinCell_textBox.Size = new System.Drawing.Size(46, 20);
            this.MinCell_textBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "MIN CELL:";
            // 
            // CellsSum_textBox
            // 
            this.CellsSum_textBox.Location = new System.Drawing.Point(67, 129);
            this.CellsSum_textBox.Name = "CellsSum_textBox";
            this.CellsSum_textBox.Size = new System.Drawing.Size(46, 20);
            this.CellsSum_textBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "CELLS";
            // 
            // swr_textBox
            // 
            this.swr_textBox.Location = new System.Drawing.Point(67, 98);
            this.swr_textBox.Name = "swr_textBox";
            this.swr_textBox.Size = new System.Drawing.Size(46, 20);
            this.swr_textBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "SWR:";
            // 
            // alt_textBox
            // 
            this.alt_textBox.Location = new System.Drawing.Point(67, 71);
            this.alt_textBox.Name = "alt_textBox";
            this.alt_textBox.Size = new System.Drawing.Size(46, 20);
            this.alt_textBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "ALTITUDE:";
            // 
            // RSSI_textBox
            // 
            this.RSSI_textBox.Location = new System.Drawing.Point(67, 44);
            this.RSSI_textBox.Name = "RSSI_textBox";
            this.RSSI_textBox.Size = new System.Drawing.Size(46, 20);
            this.RSSI_textBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "RSSI:";
            // 
            // Batt_textBox
            // 
            this.Batt_textBox.Location = new System.Drawing.Point(67, 17);
            this.Batt_textBox.Name = "Batt_textBox";
            this.Batt_textBox.Size = new System.Drawing.Size(46, 20);
            this.Batt_textBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "BATT:";
            // 
            // tbData
            // 
            this.tbData.Location = new System.Drawing.Point(447, 26);
            this.tbData.Multiline = true;
            this.tbData.Name = "tbData";
            this.tbData.Size = new System.Drawing.Size(464, 425);
            this.tbData.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(155, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "CELL 1:";
            // 
            // Cell1_textBox
            // 
            this.Cell1_textBox.Location = new System.Drawing.Point(213, 71);
            this.Cell1_textBox.Name = "Cell1_textBox";
            this.Cell1_textBox.Size = new System.Drawing.Size(49, 20);
            this.Cell1_textBox.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(155, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "CELL 2:";
            // 
            // Cell2_textBox
            // 
            this.Cell2_textBox.Location = new System.Drawing.Point(213, 97);
            this.Cell2_textBox.Name = "Cell2_textBox";
            this.Cell2_textBox.Size = new System.Drawing.Size(49, 20);
            this.Cell2_textBox.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(155, 128);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "CELL 3:";
            // 
            // Cell3_textBox
            // 
            this.Cell3_textBox.Location = new System.Drawing.Point(213, 125);
            this.Cell3_textBox.Name = "Cell3_textBox";
            this.Cell3_textBox.Size = new System.Drawing.Size(49, 20);
            this.Cell3_textBox.TabIndex = 21;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 475);
            this.Controls.Add(this.tbData);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Taranis Telemetry Receiver";
            ((System.ComponentModel.ISupportInitialize)(this.serialSettingsBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource serialSettingsBindingSource;
        private System.Windows.Forms.ComboBox baudRateComboBox;
        private System.Windows.Forms.ComboBox dataBitsComboBox;
        private System.Windows.Forms.ComboBox parityComboBox;
        private System.Windows.Forms.ComboBox portNameComboBox;
        private System.Windows.Forms.ComboBox stopBitsComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox RSSI_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Batt_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbData;
        private System.Windows.Forms.TextBox alt_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox swr_textBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CellsSum_textBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox MinCell_textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox VFAS_textBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox VFAS_MIN_textBox;
        private System.Windows.Forms.TextBox Cell3_textBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Cell2_textBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox Cell1_textBox;
        private System.Windows.Forms.Label label9;
    }
}

