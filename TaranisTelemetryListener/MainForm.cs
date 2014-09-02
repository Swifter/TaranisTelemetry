using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TaranisTelemetryListener.Serial;
using System.IO;

using log4net;

namespace TaranisTelemetryListener
{
    public partial class MainForm : Form
    {
        SerialPortManager _spManager;

        FrskyTelemetryViewModel _vm;

        private static readonly ILog log = LogManager.GetLogger(typeof(MainForm));

        public MainForm()
        {
            InitializeComponent();

            log4net.Config.XmlConfigurator.Configure();

            UserInitialization();
        }

      
        private void UserInitialization()
        {
            

            _spManager = new SerialPortManager();

            // make the view model for our data from Taranis
            _vm = new FrskyTelemetryViewModel();

            // inject the view model into the TaranisTelemetry static class
            // so we can display received updates from Taranis
            TaranisTelemetry.DependentViewModel = _vm;

            Batt_textBox.DataBindings.Add( new Binding("TEXT", _vm, "BATT_ID"));
            RSSI_textBox.DataBindings.Add( new Binding("TEXT", _vm, "RSSI"));
            alt_textBox.DataBindings.Add(new Binding("TEXT", _vm, "BARO_ALTITUDE"));
            swr_textBox.DataBindings.Add(new Binding("TEXT", _vm, "SWR"));
            CellsSum_textBox.DataBindings.Add(new Binding("Text", _vm, "CELLS_SUM"));
            MinCell_textBox.DataBindings.Add(new Binding("Text", _vm, "MIN_CELL"));
            VFAS_textBox.DataBindings.Add(new Binding("Text", _vm, "VARIO_SPEED"));
            VFAS_MIN_textBox.DataBindings.Add(new Binding("TEXT", _vm, "ADC1"));
            Cell1_textBox.DataBindings.Add(new Binding("TEXT", _vm, "CELL1"));
            Cell2_textBox.DataBindings.Add(new Binding("TEXT", _vm, "CELL2"));
            Cell3_textBox.DataBindings.Add(new Binding("TEXT", _vm, "CELL3"));



            SerialSettings mySerialSettings = _spManager.CurrentSerialSettings;
            serialSettingsBindingSource.DataSource = mySerialSettings;
            portNameComboBox.DataSource = mySerialSettings.PortNameCollection;
            baudRateComboBox.DataSource = mySerialSettings.BaudRateCollection;
            dataBitsComboBox.DataSource = mySerialSettings.DataBitsCollection;
            parityComboBox.DataSource = Enum.GetValues(typeof(System.IO.Ports.Parity));
            stopBitsComboBox.DataSource = Enum.GetValues(typeof(System.IO.Ports.StopBits));

            _spManager.NewSerialDataRecieved += new EventHandler<SerialDataEventArgs>(_spManager_NewSerialDataRecieved);
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);

            log.Debug("Made it through UserInitialization.");
        }

        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _spManager.Dispose();   
        }

        void _spManager_NewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            if (this.InvokeRequired)
            {
                // Using this.Invoke causes deadlock when closing serial port, and BeginInvoke is good practice anyway.
                this.BeginInvoke(new EventHandler<SerialDataEventArgs>(_spManager_NewSerialDataRecieved), new object[] { sender, e });
                return;
            }

            //int maxTextLength = 10000; // maximum text length in text box
            //if (tbData.TextLength > maxTextLength)
            //    tbData.Text = tbData.Text.Remove(0, tbData.TextLength - maxTextLength);

            //// This application is connected to a GPS sending ASCCI characters, so data is converted to text
            ////string str = Encoding.ASCII.GetString(e.Data);

            //string str = BitConverter.ToString(e.Data);
            //tbData.AppendText("New Packet:");
            //tbData.AppendText(str);
            //tbData.AppendText(Environment.NewLine);
            //tbData.ScrollToCaret();

            //log.Debug(str);

            TaranisTelemetry.ProcessSerialData(e.Data);


        }

        // Handles the "Start Listening"-buttom click event
        private void btnStart_Click(object sender, EventArgs e)
        {
            _spManager.StartListening();
        }

        // Handles the "Stop Listening"-buttom click event
        private void btnStop_Click(object sender, EventArgs e)
        {
            _spManager.StopListening();
        }
    }
}
