using System.Drawing;
using System.Windows.Forms;

namespace QACORDMS.Client.Helpers
{
    public static class ThemeHelper
    {
        private static readonly Color Primary = Color.FromArgb(37, 99, 235);
        private static readonly Color PrimaryDark = Color.FromArgb(30, 64, 175);
        private static readonly Color Text = Color.FromArgb(17, 24, 39);
        private static readonly Color SubtleText = Color.FromArgb(55, 65, 81);
        private static readonly Color Background = Color.FromArgb(245, 247, 250);
        private static readonly Color Surface = Color.FromArgb(250, 252, 255);
        private static readonly Color Border = Color.FromArgb(226, 232, 240);

        public static void Apply(Form form)
        {
            form.BackColor = Background;
            form.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            if (form.MainMenuStrip != null)
            {
                StyleMenuStrip(form.MainMenuStrip);
            }

            ApplyControls(form.Controls);
        }

        private static void ApplyControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                StyleControl(control);

                // Recurse into children
                if (control.HasChildren)
                {
                    ApplyControls(control.Controls);
                }
            }
        }

        private static void StyleControl(Control control)
        {
            switch (control)
            {
                case Button btn:
                    btn.BackColor = Primary;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = PrimaryDark;
                    btn.FlatAppearance.MouseOverBackColor = PrimaryDark;
                    btn.Font = new Font("Segoe UI Semibold", btn.Font.Size, FontStyle.Bold);
                    break;

                case Label lbl:
                    lbl.ForeColor = Text;
                    lbl.Font = new Font("Segoe UI Semibold", lbl.Font.Size, lbl.Font.Style);
                    break;

                case TextBox tb:
                    tb.BackColor = Color.White;
                    tb.ForeColor = Text;
                    tb.BorderStyle = BorderStyle.FixedSingle;
                    break;

                case ComboBox cb:
                    cb.BackColor = Color.White;
                    cb.ForeColor = Text;
                    cb.FlatStyle = FlatStyle.Flat;
                    break;

                case ListView lv:
                    lv.BackColor = Surface;
                    lv.ForeColor = Text;
                    lv.BorderStyle = BorderStyle.FixedSingle;
                    lv.GridLines = true;
                    break;

                case TreeView tv:
                    tv.BackColor = Surface;
                    tv.ForeColor = SubtleText;
                    tv.BorderStyle = BorderStyle.FixedSingle;
                    break;

                case DataGridView dgv:
                    dgv.BackgroundColor = Surface;
                    dgv.BorderStyle = BorderStyle.FixedSingle;
                    dgv.GridColor = Border;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = Surface;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Text;
                    dgv.DefaultCellStyle.BackColor = Color.White;
                    dgv.DefaultCellStyle.ForeColor = Text;
                    dgv.DefaultCellStyle.SelectionBackColor = Primary;
                    dgv.DefaultCellStyle.SelectionForeColor = Color.White;
                    break;

                case StatusStrip strip:
                    strip.BackColor = Color.White;
                    strip.ForeColor = SubtleText;
                    foreach (ToolStripItem item in strip.Items)
                    {
                        item.ForeColor = SubtleText;
                    }
                    break;

                case ToolStrip ts:
                    ts.BackColor = Color.White;
                    foreach (ToolStripItem item in ts.Items)
                    {
                        item.ForeColor = Primary;
                    }
                    break;

                case Panel pnl when pnl.Name == "loaderOverlay":
                    // Keep loader overlay transparent feel
                    pnl.BackColor = Color.FromArgb(60, 255, 255, 255);
                    break;

                default:
                    control.BackColor = control.BackColor == Color.Transparent ? Color.Transparent : control.BackColor;
                    break;
            }
        }

        private static void StyleMenuStrip(MenuStrip menuStrip)
        {
            menuStrip.BackColor = Color.White;
            menuStrip.ForeColor = Text;
            menuStrip.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);

            foreach (ToolStripItem item in menuStrip.Items)
            {
                item.ForeColor = Text;
            }
        }
    }
}
