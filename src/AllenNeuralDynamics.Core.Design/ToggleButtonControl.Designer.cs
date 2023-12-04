namespace AllenNeuralDynamics.Core.Design
{
    partial class ToggleButtonStateControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.togglePanel = new System.Windows.Forms.Panel();
            this.toggleButton = new System.Windows.Forms.CheckBox();
            this.togglePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // togglePanel
            // 
            this.togglePanel.Controls.Add(this.toggleButton);
            this.togglePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.togglePanel.Location = new System.Drawing.Point(0, 0);
            this.togglePanel.Margin = new System.Windows.Forms.Padding(6);
            this.togglePanel.Name = "togglePanel";
            this.togglePanel.Size = new System.Drawing.Size(400, 240);
            this.togglePanel.TabIndex = 10;
            // 
            // toggleButton
            // 
            this.toggleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toggleButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.toggleButton.Location = new System.Drawing.Point(6, 6);
            this.toggleButton.Margin = new System.Windows.Forms.Padding(6);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(388, 228);
            this.toggleButton.TabIndex = 0;
            this.toggleButton.Text = "Maintenance";
            this.toggleButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toggleButton.UseVisualStyleBackColor = true;
            this.toggleButton.CheckedChanged += new System.EventHandler(this.maintenanceButton_CheckedChanged);
            // 
            // ToggleButtonStateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.togglePanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ToggleButtonStateControl";
            this.Size = new System.Drawing.Size(400, 240);
            this.togglePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel togglePanel;
        private System.Windows.Forms.CheckBox toggleButton;
    }
}
