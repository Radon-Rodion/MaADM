using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaADM.Common;
using MaADM.Algorythms;

namespace MaADM
{
    public partial class Form1 : Form
    {
        FieldController controller;

        public Form1()
        {
            InitializeComponent();
            controller = new FieldController(this.pictureBox1);
        }

        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGo = new System.Windows.Forms.Button();
            this.maximinRB = new System.Windows.Forms.RadioButton();
            this.kMiddleRB = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.clearBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(348, 261);
            this.splitContainer1.SplitterDistance = 55;
            this.splitContainer1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.btnGo);
            this.flowLayoutPanel1.Controls.Add(this.clearBtn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(348, 55);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnGo
            // 
            this.btnGo.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnGo.Location = new System.Drawing.Point(187, 3);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 50);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.BtnGo_Click);
            // 
            // maximinRB
            // 
            this.maximinRB.AutoSize = true;
            this.maximinRB.Dock = System.Windows.Forms.DockStyle.Left;
            this.maximinRB.Location = new System.Drawing.Point(68, 16);
            this.maximinRB.Name = "maximinRB";
            this.maximinRB.Size = new System.Drawing.Size(63, 31);
            this.maximinRB.TabIndex = 1;
            this.maximinRB.Text = "Maximin";
            this.maximinRB.UseVisualStyleBackColor = true;
            this.maximinRB.CheckedChanged += new System.EventHandler(this.MaximinRB_CheckedChanged);
            // 
            // kMiddleRB
            // 
            this.kMiddleRB.AutoSize = true;
            this.kMiddleRB.Checked = true;
            this.kMiddleRB.Dock = System.Windows.Forms.DockStyle.Left;
            this.kMiddleRB.Location = new System.Drawing.Point(3, 16);
            this.kMiddleRB.Name = "kMiddleRB";
            this.kMiddleRB.Size = new System.Drawing.Size(65, 31);
            this.kMiddleRB.TabIndex = 0;
            this.kMiddleRB.TabStop = true;
            this.kMiddleRB.Text = "K-middle";
            this.kMiddleRB.UseVisualStyleBackColor = true;
            this.kMiddleRB.CheckedChanged += new System.EventHandler(this.KMiddleRB_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.maximinRB);
            this.groupBox1.Controls.Add(this.kMiddleRB);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.MinimumSize = new System.Drawing.Size(0, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modes";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(348, 202);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseClick);
            // 
            // clearBtn
            // 
            this.clearBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.clearBtn.Location = new System.Drawing.Point(268, 3);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(75, 50);
            this.clearBtn.TabIndex = 3;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(348, 261);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "MaADM labs by Rafeev";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        private void BtnGo_Click(object sender, EventArgs e)
        {
            controller.Go();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            controller.ClearFieldAndGenerateElements();
        }

        private void KMiddleRB_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                controller.Algorythm = new KMiddleAlgorythm();
        }

        private void MaximinRB_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                controller.Algorythm = new MaximinAlgorythm();
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            controller.OnFieldClick(e.Location);
        }
    }
}
