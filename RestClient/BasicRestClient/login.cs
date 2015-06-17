using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicRestClient
{
    class Item
    {
        public string Name;
        public int Value;
        public Item(string name, int value)
        {
            Name = name; Value = value;
        }
        public override string ToString()
        {
            // Generates the text shown in the combo box
            return Name;
        }
    }


    class login : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBxUser;
        private System.Windows.Forms.TextBox txtBoxPass;
        private Label label3;
        private ComboBox cmbBxServer;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Button btnLogin;
        public static string USER { get; set; }
        public string PASS { get; set; }
        public bool LOADED { get; set; }
        public string SERVER { get; set; }
        
    
        public login()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxUser = new System.Windows.Forms.TextBox();
            this.txtBoxPass = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbBxServer = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(86, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(86, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // txtBxUser
            // 
            this.txtBxUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxUser.Location = new System.Drawing.Point(89, 153);
            this.txtBxUser.Name = "txtBxUser";
            this.txtBxUser.Size = new System.Drawing.Size(242, 29);
            this.txtBxUser.TabIndex = 2;
            // 
            // txtBoxPass
            // 
            this.txtBoxPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxPass.Location = new System.Drawing.Point(89, 220);
            this.txtBoxPass.Name = "txtBoxPass";
            this.txtBoxPass.PasswordChar = '*';
            this.txtBoxPass.Size = new System.Drawing.Size(242, 29);
            this.txtBoxPass.TabIndex = 3;
            this.txtBoxPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxPass_KeyPress);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(127, 278);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(102, 43);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(86, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "Server Address";
            // 
            // cmbBxServer
            // 
            this.cmbBxServer.DisplayMember = "Value";
            this.cmbBxServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBxServer.FormattingEnabled = true;
            cmbBxServer.Items.Add(new Item("http://192.168.28.128:8050", 1));
            cmbBxServer.Items.Add(new Item("https://192.168.28.128", 2));
            cmbBxServer.Items.Add(new Item("http://localhost:54719/", 3));
            //cmbBxServer.Items.Add(new Item("http://161.28.110.227:8050", 1));
            //cmbBxServer.Items.Add(new Item("https://161.28.110.227", 2));
            this.cmbBxServer.Location = new System.Drawing.Point(89, 65);
            this.cmbBxServer.Name = "cmbBxServer";
            this.cmbBxServer.Size = new System.Drawing.Size(242, 32);
            this.cmbBxServer.TabIndex = 7;
            this.cmbBxServer.ValueMember = "Value";
            this.cmbBxServer.SelectedIndexChanged += new System.EventHandler(this.cmbBxServer_SelectedIndexChanged);
            // 
            // login
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(409, 362);
            this.Controls.Add(this.cmbBxServer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtBoxPass);
            this.Controls.Add(this.txtBxUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "login";
            this.Opacity = 0.9D;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtBoxPass != null & txtBxUser != null)
            {
                USER = txtBxUser.Text;
                PASS = txtBoxPass.Text;
                SERVER = cmbBxServer.Text;
                this.Close();
            }
        }

        private void txtBoxPass_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                btnLogin_Click(this, null);
            }
        }

        private void cmbBxServer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
