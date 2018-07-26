using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace autocaddayin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
                private void button3_Click(object sender, EventArgs e)
        {
            AutoCAD.AcadApplication newapp = new AutoCAD.AcadApplication();
            AutoCAD.AcadDocument doc = null;

            newapp.Application.Visible = true;
            try
            {
                doc = newapp.Documents.Open(this.textBox1.Text, Type.Missing, Type.Missing);

                AutoCAD.AcadLayout layout = doc.ActiveLayout;

                AutoCAD.AcadPlotConfiguration oplot = doc.PlotConfigurations.Add("PDF", layout.ModelType);
                oplot.PaperUnits = AutoCAD.AcPlotPaperUnits.acMillimeters;
                oplot.StyleSheet = "monochrome.ctb";
                oplot.PlotWithPlotStyles = true;
                oplot.ConfigName = "DWG To PDF.pc3";
                oplot.UseStandardScale = true;
                oplot.StandardScale = AutoCAD.AcPlotScale.acScaleToFit;
                oplot.PlotType = AutoCAD.AcPlotType.acExtents;
                oplot.CenterPlot = true;


                layout.CopyFrom(oplot);
                layout.PlotRotation = AutoCAD.AcPlotRotation.ac0degrees;
                layout.RefreshPlotDeviceInfo();
                doc.SetVariable("BACKGROUNDPLOT", 0);
                doc.Plot.QuietErrorMode = true;
                doc.Plot.PlotToFile(this.textBox2.Text, "DWG To PDF.pc3");
                oplot.Delete();
                oplot = null;

            }
            catch{}
        }
    }
}
