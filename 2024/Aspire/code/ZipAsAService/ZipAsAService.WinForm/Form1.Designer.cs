namespace ZipAsAService.WinForm;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        dgWeather = new DataGridView();
        pbLoading = new ProgressBar();
        btnLoadWeather = new Button();
        button1 = new Button();
        txtZip = new TextBox();
        ((System.ComponentModel.ISupportInitialize)dgWeather).BeginInit();
        SuspendLayout();
        // 
        // dgWeather
        // 
        dgWeather.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgWeather.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgWeather.Location = new Point(359, 349);
        dgWeather.Margin = new Padding(5);
        dgWeather.Name = "dgWeather";
        dgWeather.ReadOnly = true;
        dgWeather.RowHeadersWidth = 51;
        dgWeather.Size = new Size(1315, 725);
        dgWeather.TabIndex = 6;
        // 
        // pbLoading
        // 
        pbLoading.Cursor = Cursors.IBeam;
        pbLoading.Location = new Point(604, 245);
        pbLoading.Margin = new Padding(5);
        pbLoading.Name = "pbLoading";
        pbLoading.Size = new Size(203, 46);
        pbLoading.Style = ProgressBarStyle.Continuous;
        pbLoading.TabIndex = 5;
        pbLoading.Visible = false;
        // 
        // btnLoadWeather
        // 
        btnLoadWeather.Location = new Point(359, 245);
        btnLoadWeather.Margin = new Padding(5);
        btnLoadWeather.Name = "btnLoadWeather";
        btnLoadWeather.Size = new Size(236, 46);
        btnLoadWeather.TabIndex = 4;
        btnLoadWeather.Text = "Load Weather";
        btnLoadWeather.UseVisualStyleBackColor = true;
        btnLoadWeather.Click += btnLoadWeather_Click;
        // 
        // button1
        // 
        button1.Location = new Point(876, 245);
        button1.Margin = new Padding(5);
        button1.Name = "button1";
        button1.Size = new Size(236, 46);
        button1.TabIndex = 7;
        button1.Text = "ZIP";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // txtZip
        // 
        txtZip.Location = new Point(1178, 253);
        txtZip.Name = "txtZip";
        txtZip.Size = new Size(496, 39);
        txtZip.TabIndex = 8;
        txtZip.Text = "Andrei Ignat";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(13F, 32F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(2032, 1318);
        Controls.Add(txtZip);
        Controls.Add(button1);
        Controls.Add(dgWeather);
        Controls.Add(pbLoading);
        Controls.Add(btnLoadWeather);
        Name = "Form1";
        Text = "Form1";
        ((System.ComponentModel.ISupportInitialize)dgWeather).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private DataGridView dgWeather;
    private ProgressBar pbLoading;
    private Button btnLoadWeather;
    private Button button1;
    private TextBox txtZip;
}
