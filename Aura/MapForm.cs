using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Aura
{
    public partial class MapForm : Form
    {
        private readonly IEnumerable<ReportingResult> _results;
        private readonly XDocument _config;

        public MapForm(IEnumerable<ReportingResult> results)
        {
            _results = results;
            InitializeComponent();

            _config = XDocument.Load("map.xml");
        }

        private void OnClick(object sender, EventArgs e)
        {
            #if DEBUG
            var ee = e as MouseEventArgs;
            if(ee != null)
            {
                MessageBox.Show(string.Format("x: {0}, y: {1}", ee.X, ee.Y));
                Clipboard.SetText(string.Format("x=\"{0}\" y=\"{1}\"", ee.X, ee.Y));
            }
            #endif
        }

        private void MapForm_Paint(object sender, PaintEventArgs e)
        {
            var points = _config
                            .Root
                            .Elements("point")
                            .Select(point => new
                                {
                                    Id = long.Parse(point.Attribute("id").Value),
                                    X = int.Parse(point.Attribute("x").Value),
                                    Y = int.Parse(point.Attribute("y").Value)
                                })
                            .Join(_results, point => point.Id, result => result.Id, (point, result) => new { point.X, point.Y, result.A1, result.A2, result.A3, result.A4})
                            .ToArray();

            var maxA1 = points.Max(point => point.A1);
            var maxA2 = points.Max(point => point.A2);
            var maxA3 = points.Max(point => point.A3);
            var maxA4 = points.Max(point => point.A4);

            const int heaght = 50;
            const int widht = 20;

            foreach (var point in points)
            {
                var pen = new Pen(Color.Black, 1);

                var brush1 = new SolidBrush(Color.Red);
                var h1 = (int)(point.A1 * heaght / maxA1) + 1;
                e.Graphics.FillRectangle(brush1, new Rectangle(point.X, point.Y /*+ heaght*/ - h1, widht, h1));
                e.Graphics.DrawRectangle(pen, new Rectangle(point.X, point.Y /*+ heaght*/ - h1, widht, h1));

                var brush2 = new SolidBrush(Color.Green);
                var h2 = (int) (point.A2*heaght/maxA2) + 1;
                e.Graphics.FillRectangle(brush2, new Rectangle(point.X + widht, point.Y /*+ heaght*/ - h2, widht, h2));
                e.Graphics.DrawRectangle(pen, new Rectangle(point.X + widht, point.Y /*+ heaght*/ - h2, widht, h2));

                var brush3 = new SolidBrush(Color.Blue);
                var h3 = (int)(point.A3 * heaght / maxA3) + 1;
                e.Graphics.FillRectangle(brush3, new Rectangle(point.X + 2 * widht, point.Y /*+ heaght*/ - h3, widht, h3));
                e.Graphics.DrawRectangle(pen, new Rectangle(point.X + 2 * widht, point.Y /*+ heaght*/ - h3, widht, h3));

                var brush4 = new SolidBrush(Color.Yellow);
                var h4 = (int)(point.A4 * heaght / maxA4) + 1;
                e.Graphics.FillRectangle(brush4, new Rectangle(point.X + 3 * widht, point.Y /*+ heaght*/ - h4, widht, h4));
                e.Graphics.DrawRectangle(pen, new Rectangle(point.X + 3 * widht, point.Y /*+ heaght*/ - h4, widht, h4));
            }
        }
    }
}
