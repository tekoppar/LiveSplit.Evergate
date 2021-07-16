namespace LiveSplit.Evergate {
    partial class Manager {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblIn = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblCanDash = new System.Windows.Forms.Label();
            this.lblPos = new System.Windows.Forms.Label();
            this.lblScene = new System.Windows.Forms.Label();
            this.lblCanJump = new System.Windows.Forms.Label();
            this.lblFPS = new System.Windows.Forms.Label();
            this.lblExtra = new System.Windows.Forms.Label();
            this.tooltips = new System.Windows.Forms.ToolTip(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNote
            // 
            this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(500, 0);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(371, 194);
            this.lblNote.TabIndex = 15;
            this.lblNote.Text = "Not available";
            this.lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIn
            // 
            this.lblIn.AutoSize = true;
            this.lblIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIn.Location = new System.Drawing.Point(3, 121);
            this.lblIn.Name = "lblIn";
            this.lblIn.Size = new System.Drawing.Size(57, 20);
            this.lblIn.TabIndex = 25;
            this.lblIn.Text = "In: N/A";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpeed.Location = new System.Drawing.Point(3, 61);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(60, 20);
            this.lblSpeed.TabIndex = 24;
            this.lblSpeed.Text = "Speed:";
            // 
            // lblCanDash
            // 
            this.lblCanDash.AutoSize = true;
            this.lblCanDash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCanDash.Location = new System.Drawing.Point(3, 81);
            this.lblCanDash.Name = "lblCanDash";
            this.lblCanDash.Size = new System.Drawing.Size(114, 20);
            this.lblCanDash.TabIndex = 20;
            this.lblCanDash.Text = "Can Dash: N/A";
            // 
            // lblPos
            // 
            this.lblPos.AutoSize = true;
            this.lblPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPos.Location = new System.Drawing.Point(3, 41);
            this.lblPos.Name = "lblPos";
            this.lblPos.Size = new System.Drawing.Size(69, 20);
            this.lblPos.TabIndex = 17;
            this.lblPos.Text = "Position:";
            // 
            // lblScene
            // 
            this.lblScene.AutoSize = true;
            this.lblScene.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScene.Location = new System.Drawing.Point(3, 21);
            this.lblScene.Name = "lblScene";
            this.lblScene.Size = new System.Drawing.Size(59, 20);
            this.lblScene.TabIndex = 26;
            this.lblScene.Text = "Scene:";
            // 
            // lblCanJump
            // 
            this.lblCanJump.AutoSize = true;
            this.lblCanJump.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCanJump.Location = new System.Drawing.Point(3, 101);
            this.lblCanJump.Name = "lblCanJump";
            this.lblCanJump.Size = new System.Drawing.Size(115, 20);
            this.lblCanJump.TabIndex = 27;
            this.lblCanJump.Text = "Can Jump: N/A";
            // 
            // lblFPS
            // 
            this.lblFPS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFPS.Location = new System.Drawing.Point(3, 0);
            this.lblFPS.Name = "lblFPS";
            this.lblFPS.Size = new System.Drawing.Size(70, 21);
            this.lblFPS.TabIndex = 28;
            this.lblFPS.Text = "FPS: 0.0";
            // 
            // lblExtra
            // 
            this.lblExtra.AutoSize = true;
            this.lblExtra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtra.Location = new System.Drawing.Point(3, 141);
            this.lblExtra.Name = "lblExtra";
            this.lblExtra.Size = new System.Drawing.Size(309, 20);
            this.lblExtra.TabIndex = 29;
            this.lblExtra.Text = "Debug: Off   No Pause: Off   FPS Lock: Off";
            this.tooltips.SetToolTip(this.lblExtra, "Ctrl+D Debug\r\nCtrl+N NoPause\r\nCtrl+F Lock FPS");
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lblFPS);
            this.flowLayoutPanel1.Controls.Add(this.lblScene);
            this.flowLayoutPanel1.Controls.Add(this.lblPos);
            this.flowLayoutPanel1.Controls.Add(this.lblSpeed);
            this.flowLayoutPanel1.Controls.Add(this.lblCanDash);
            this.flowLayoutPanel1.Controls.Add(this.lblCanJump);
            this.flowLayoutPanel1.Controls.Add(this.lblIn);
            this.flowLayoutPanel1.Controls.Add(this.lblExtra);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 5);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(330, 167);
            this.flowLayoutPanel1.TabIndex = 30;
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(330, 172);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblNote);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::LiveSplit.Evergate.Properties.Resources.evergate;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Manager";
            this.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Manager";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Manager_KeyDown);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblIn;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblCanDash;
        private System.Windows.Forms.Label lblPos;
        private System.Windows.Forms.Label lblScene;
        private System.Windows.Forms.Label lblCanJump;
        private System.Windows.Forms.Label lblFPS;
        private System.Windows.Forms.Label lblExtra;
        private System.Windows.Forms.ToolTip tooltips;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}