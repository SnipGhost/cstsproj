namespace tsproj.test_logic
{
    using System;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public class tools
    {
        private static Control em_container;
        private static Panel em_panel;

        public static void CleanEmsg()
        {
            if ((em_container != null) && (em_panel != null))
            {
                em_container.Height -= em_panel.Height;
                em_container.Controls.Remove(em_panel);
                em_container = null;
                em_panel = null;
            }
        }

        public static void ErrorMsg(string msg, Control container, DockStyle dock)
        {
            if (em_panel == null)
            {
                em_panel = new Panel();
                em_panel.BackColor = Color.LightCoral;
                em_panel.Height = 40;
                em_panel.Dock = dock;
                Label label = new Label {
                    Text = msg,
                    Font = new Font("Microsoft Sans Serif", 20f, FontStyle.Bold),
                    Dock = DockStyle.Top,
                    AutoSize = true
                };
                em_panel.Controls.Add(label);
                em_container = container;
                em_container.Controls.Add(em_panel);
                em_container.Height += em_panel.Height;
            }
        }

        public static void InfoMsg(string msg, Control container, DockStyle dock, int H)
        {
            if (em_panel == null)
            {
                em_panel = new Panel();
                em_panel.BackColor = Color.LightBlue;
                em_panel.Height = 40 * H;
                em_panel.Dock = dock;
                Label label = new Label {
                    Text = msg,
                    Font = new Font("Microsoft Sans Serif", 20f, FontStyle.Bold),
                    Dock = DockStyle.Fill,
                    AutoSize = true
                };
                em_panel.Controls.Add(label);
                em_container = container;
                em_container.Controls.Add(em_panel);
                em_container.Height += em_panel.Height;
            }
        }

        public static string repls(string iss)
        {
            StringBuilder builder = new StringBuilder(iss);
            builder.Replace(";", "");
            builder.Replace("А)", "");
            builder.Replace("Б)", "");
            builder.Replace("В)", "");
            builder.Replace("Г)", "");
            builder.Replace("Д)", "");
            builder.Replace("Е)", "");
            builder.Replace("A)", "");
            builder.Replace("B)", "");
            builder.Replace("C)", "");
            builder.Replace("D)", "");
            builder.Replace("E)", "");
            builder.Replace("F)", "");
            builder.Replace("1)", "");
            builder.Replace("2)", "");
            builder.Replace("3)", "");
            builder.Replace("4)", "");
            builder.Replace("5)", "");
            builder.Replace("6)", "");
            builder.Replace("1.", "");
            builder.Replace("2.", "");
            builder.Replace("3.", "");
            builder.Replace("4.", "");
            builder.Replace("5.", "");
            builder.Replace("6.", "");
            return builder.ToString();
        }

        public static int text_en(string s)
        {
            int num = 0;
            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];
                num += ch.GetHashCode() + ((7 * i) * i);
            }
            return num;
        }
    }
}

