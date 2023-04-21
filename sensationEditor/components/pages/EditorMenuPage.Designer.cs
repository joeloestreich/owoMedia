﻿namespace owoMedia.sensationEditor.components.pages {
    partial class EditorMenuPage {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnNewTrack = new System.Windows.Forms.Button();
            this.btnEditTrack = new System.Windows.Forms.Button();
            this.btnTemplates = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNewTrack
            // 
            this.btnNewTrack.Location = new System.Drawing.Point(3, 210);
            this.btnNewTrack.Name = "btnNewTrack";
            this.btnNewTrack.Size = new System.Drawing.Size(499, 87);
            this.btnNewTrack.TabIndex = 4;
            this.btnNewTrack.Text = "New Sensation Track";
            this.btnNewTrack.UseVisualStyleBackColor = true;
            this.btnNewTrack.Click += new System.EventHandler(this.btnNewTrack_Click);
            // 
            // btnEditTrack
            // 
            this.btnEditTrack.Location = new System.Drawing.Point(3, 303);
            this.btnEditTrack.Name = "btnEditTrack";
            this.btnEditTrack.Size = new System.Drawing.Size(499, 87);
            this.btnEditTrack.TabIndex = 5;
            this.btnEditTrack.Text = "Edit Sensation Track";
            this.btnEditTrack.UseVisualStyleBackColor = true;
            this.btnEditTrack.Click += new System.EventHandler(this.btnEditTrack_Click);
            // 
            // btnTemplates
            // 
            this.btnTemplates.Location = new System.Drawing.Point(3, 456);
            this.btnTemplates.Name = "btnTemplates";
            this.btnTemplates.Size = new System.Drawing.Size(499, 87);
            this.btnTemplates.TabIndex = 6;
            this.btnTemplates.Text = "Manage Sensations Templates";
            this.btnTemplates.UseVisualStyleBackColor = true;
            this.btnTemplates.Click += new System.EventHandler(this.btnTemplates_Click);
            // 
            // EditorMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnTemplates);
            this.Controls.Add(this.btnEditTrack);
            this.Controls.Add(this.btnNewTrack);
            this.Name = "EditorMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNewTrack;
        private System.Windows.Forms.Button btnEditTrack;
        private System.Windows.Forms.Button btnTemplates;
    }
}