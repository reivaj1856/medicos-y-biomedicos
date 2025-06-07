using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using medicos_y_biomedicos.Formularios;

namespace medicos_y_biomedicos
{
    public partial class Form1 : Form
    {
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;
        public Form1()
        {
            InitializeComponent();
            customizeDesing();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        #region menu
        private void customizeDesing()
        {
            panelVenta.Visible = false; 
            panelregistros.Visible = false;
        }
        private void hideSubMenu()
        {
            if (panelVenta.Visible==true) {
                panelVenta.Visible = false;
            }
            if (panelregistros.Visible == true)
            {
                panelregistros.Visible = false;
            }
            if (panelRegistro.Visible == true)
            {
                panelRegistro.Visible = false;
            }
        }
        private void showsubMenu(Panel panelsubMenu)
        {
            if (panelsubMenu.Visible == false) { 
                hideSubMenu();
                panelsubMenu.Visible = true;
            }
            else
            {
                panelsubMenu.Visible = false;
            }
        }
        

        #region redibujado
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            this.panelContenedor.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }
        #endregion
        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnventa_Click(object sender, EventArgs e)
        {
            showsubMenu(panelVenta);
        }

        private void btnequipos_Click(object sender, EventArgs e)
        {
            AbrirFormulario<VentanaVenta>();
            hideSubMenu();
        }

        private void btnregistros_Click(object sender, EventArgs e)
        {
            showsubMenu(panelregistros);
        }

        private void btnreventa_Click(object sender, EventArgs e)
        {
            AbrirFormulario<RegistroVenta>();
            hideSubMenu();
        }

        private void btnmantenimiento_Click(object sender, EventArgs e)
        {
            AbrirFormulario<ResgistroMantenimiento>();
            hideSubMenu();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            showsubMenu(panelRegistro);
        }
        private void btnReEquipo_Click(object sender, EventArgs e)
        {
            AbrirFormulario<RegistrarEquipo>();
            hideSubMenu();
        }
        private void btnReMantenimiento_Click(object sender, EventArgs e)
        {
            AbrirFormulario<RegistrarMantenimiento>();
            hideSubMenu();
        }
        #endregion
        #region controles
        private void cerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int lx, ly;
        int sw, sh;

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }

        private void maximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion
        #region fomr contenedor
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormulario<VentanaVenta>();
            btnventa.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirFormulario<RegistroVenta>();
            btnreventa.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AbrirFormulario<ResgistroMantenimiento>();
            btnmantenimiento.BackColor = Color.FromArgb(12, 61, 92);
        }

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelsuperior_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = panelformularios.Controls.OfType<MiForm>().FirstOrDefault();//Busca en la colecion el formulario
            //si el formulario/instancia no existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelformularios.Controls.Add(formulario);
                panelformularios.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
                formulario.FormClosed += new FormClosedEventHandler(CloseForms);
            }
            //si el formulario/instancia existe
            else
            {
                formulario.BringToFront();
            }
        }
        private void CloseForms(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Form1"] == null)
                btnventa.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["Form2"] == null)
                btnreventa.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["Form3"] == null)
                btnmantenimiento.BackColor = Color.FromArgb(4, 41, 68);
        }
        #endregion
    }
}
