﻿namespace SWeight
{
    partial class FaceForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxBothTypes = new System.Windows.Forms.CheckBox();
            this.checkBoxSLI = new System.Windows.Forms.CheckBox();
            this.checkBoxLLI = new System.Windows.Forms.CheckBox();
            this.groupBoxType = new System.Windows.Forms.GroupBox();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabSamples = new System.Windows.Forms.TabPage();
            this.groupBoxSamples = new System.Windows.Forms.GroupBox();
            this.dataGridView_Samples = new System.Windows.Forms.DataGridView();
            this.groupBoxSamplesSets = new System.Windows.Forms.GroupBox();
            this.dataGridView_SamplesSet = new System.Windows.Forms.DataGridView();
            this.tabStandarts = new System.Windows.Forms.TabPage();
            this.groupBoxStandarts = new System.Windows.Forms.GroupBox();
            this.dataGridView_Standarts = new System.Windows.Forms.DataGridView();
            this.groupBoxStandartsSets = new System.Windows.Forms.GroupBox();
            this.dataGridView_StandartsSet = new System.Windows.Forms.DataGridView();
            this.tabMonitors = new System.Windows.Forms.TabPage();
            this.groupBoxMonitors = new System.Windows.Forms.GroupBox();
            this.dataGridView_Monitors = new System.Windows.Forms.DataGridView();
            this.groupBoxMonitorsSets = new System.Windows.Forms.GroupBox();
            this.dataGridView_MonitorsSet = new System.Windows.Forms.DataGridView();
            this.buttonReadFromFile = new System.Windows.Forms.Button();
            this.buttonSave2File = new System.Windows.Forms.Button();
            this.buttonAddRow = new System.Windows.Forms.Button();
            this.buttonSave2DB = new System.Windows.Forms.Button();
            this.buttonReadWeight = new System.Windows.Forms.Button();
            this.groupBoxType.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabSamples.SuspendLayout();
            this.groupBoxSamples.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Samples)).BeginInit();
            this.groupBoxSamplesSets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_SamplesSet)).BeginInit();
            this.tabStandarts.SuspendLayout();
            this.groupBoxStandarts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Standarts)).BeginInit();
            this.groupBoxStandartsSets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_StandartsSet)).BeginInit();
            this.tabMonitors.SuspendLayout();
            this.groupBoxMonitors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Monitors)).BeginInit();
            this.groupBoxMonitorsSets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_MonitorsSet)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxBothTypes
            // 
            this.checkBoxBothTypes.AutoSize = true;
            this.checkBoxBothTypes.Checked = true;
            this.checkBoxBothTypes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBothTypes.Location = new System.Drawing.Point(8, 23);
            this.checkBoxBothTypes.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxBothTypes.Name = "checkBoxBothTypes";
            this.checkBoxBothTypes.Size = new System.Drawing.Size(104, 20);
            this.checkBoxBothTypes.TabIndex = 0;
            this.checkBoxBothTypes.Text = "КЖИ и ДЖИ";
            this.checkBoxBothTypes.UseVisualStyleBackColor = true;
            // 
            // checkBoxSLI
            // 
            this.checkBoxSLI.AutoSize = true;
            this.checkBoxSLI.Location = new System.Drawing.Point(8, 52);
            this.checkBoxSLI.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxSLI.Name = "checkBoxSLI";
            this.checkBoxSLI.Size = new System.Drawing.Size(58, 20);
            this.checkBoxSLI.TabIndex = 1;
            this.checkBoxSLI.Text = "КЖИ";
            this.checkBoxSLI.UseVisualStyleBackColor = true;
            // 
            // checkBoxLLI
            // 
            this.checkBoxLLI.AutoSize = true;
            this.checkBoxLLI.Location = new System.Drawing.Point(8, 80);
            this.checkBoxLLI.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxLLI.Name = "checkBoxLLI";
            this.checkBoxLLI.Size = new System.Drawing.Size(59, 20);
            this.checkBoxLLI.TabIndex = 2;
            this.checkBoxLLI.Text = "ДЖИ";
            this.checkBoxLLI.UseVisualStyleBackColor = true;
            // 
            // groupBoxType
            // 
            this.groupBoxType.Controls.Add(this.checkBoxBothTypes);
            this.groupBoxType.Controls.Add(this.checkBoxLLI);
            this.groupBoxType.Controls.Add(this.checkBoxSLI);
            this.groupBoxType.Location = new System.Drawing.Point(963, 42);
            this.groupBoxType.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxType.Name = "groupBoxType";
            this.groupBoxType.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxType.Size = new System.Drawing.Size(141, 123);
            this.groupBoxType.TabIndex = 3;
            this.groupBoxType.TabStop = false;
            this.groupBoxType.Text = "Тип";
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabSamples);
            this.tabs.Controls.Add(this.tabStandarts);
            this.tabs.Controls.Add(this.tabMonitors);
            this.tabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabs.Location = new System.Drawing.Point(16, 15);
            this.tabs.Margin = new System.Windows.Forms.Padding(4);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(939, 731);
            this.tabs.TabIndex = 4;
            // 
            // tabSamples
            // 
            this.tabSamples.Controls.Add(this.groupBoxSamples);
            this.tabSamples.Controls.Add(this.groupBoxSamplesSets);
            this.tabSamples.Location = new System.Drawing.Point(4, 24);
            this.tabSamples.Margin = new System.Windows.Forms.Padding(4);
            this.tabSamples.Name = "tabSamples";
            this.tabSamples.Padding = new System.Windows.Forms.Padding(4);
            this.tabSamples.Size = new System.Drawing.Size(931, 703);
            this.tabSamples.TabIndex = 0;
            this.tabSamples.Text = "Образцы";
            this.tabSamples.UseVisualStyleBackColor = true;
            // 
            // groupBoxSamples
            // 
            this.groupBoxSamples.Controls.Add(this.dataGridView_Samples);
            this.groupBoxSamples.Location = new System.Drawing.Point(8, 353);
            this.groupBoxSamples.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxSamples.Name = "groupBoxSamples";
            this.groupBoxSamples.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxSamples.Size = new System.Drawing.Size(912, 338);
            this.groupBoxSamples.TabIndex = 2;
            this.groupBoxSamples.TabStop = false;
            this.groupBoxSamples.Text = "Список образцов";
            // 
            // dataGridView_Samples
            // 
            this.dataGridView_Samples.AllowUserToAddRows = false;
            this.dataGridView_Samples.AllowUserToDeleteRows = false;
            this.dataGridView_Samples.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Samples.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_Samples.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Samples.Location = new System.Drawing.Point(8, 23);
            this.dataGridView_Samples.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView_Samples.Name = "dataGridView_Samples";
            this.dataGridView_Samples.RowHeadersVisible = false;
            this.dataGridView_Samples.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_Samples.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_Samples.Size = new System.Drawing.Size(896, 308);
            this.dataGridView_Samples.TabIndex = 0;
            // 
            // groupBoxSamplesSets
            // 
            this.groupBoxSamplesSets.Controls.Add(this.dataGridView_SamplesSet);
            this.groupBoxSamplesSets.Location = new System.Drawing.Point(8, 7);
            this.groupBoxSamplesSets.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxSamplesSets.Name = "groupBoxSamplesSets";
            this.groupBoxSamplesSets.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxSamplesSets.Size = new System.Drawing.Size(912, 338);
            this.groupBoxSamplesSets.TabIndex = 1;
            this.groupBoxSamplesSets.TabStop = false;
            this.groupBoxSamplesSets.Text = "Партии образцов";
            // 
            // dataGridView_SamplesSet
            // 
            this.dataGridView_SamplesSet.AllowUserToAddRows = false;
            this.dataGridView_SamplesSet.AllowUserToDeleteRows = false;
            this.dataGridView_SamplesSet.AllowUserToOrderColumns = true;
            this.dataGridView_SamplesSet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_SamplesSet.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_SamplesSet.CausesValidation = false;
            this.dataGridView_SamplesSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_SamplesSet.Location = new System.Drawing.Point(8, 23);
            this.dataGridView_SamplesSet.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView_SamplesSet.MultiSelect = false;
            this.dataGridView_SamplesSet.Name = "dataGridView_SamplesSet";
            this.dataGridView_SamplesSet.ReadOnly = true;
            this.dataGridView_SamplesSet.RowHeadersVisible = false;
            this.dataGridView_SamplesSet.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_SamplesSet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_SamplesSet.Size = new System.Drawing.Size(896, 308);
            this.dataGridView_SamplesSet.TabIndex = 0;
            this.dataGridView_SamplesSet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_SamplesSet_CellClick);
            // 
            // tabStandarts
            // 
            this.tabStandarts.Controls.Add(this.groupBoxStandarts);
            this.tabStandarts.Controls.Add(this.groupBoxStandartsSets);
            this.tabStandarts.Location = new System.Drawing.Point(4, 24);
            this.tabStandarts.Margin = new System.Windows.Forms.Padding(4);
            this.tabStandarts.Name = "tabStandarts";
            this.tabStandarts.Padding = new System.Windows.Forms.Padding(4);
            this.tabStandarts.Size = new System.Drawing.Size(931, 703);
            this.tabStandarts.TabIndex = 1;
            this.tabStandarts.Text = "Стандарты";
            this.tabStandarts.UseVisualStyleBackColor = true;
            // 
            // groupBoxStandarts
            // 
            this.groupBoxStandarts.Controls.Add(this.dataGridView_Standarts);
            this.groupBoxStandarts.Location = new System.Drawing.Point(8, 353);
            this.groupBoxStandarts.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxStandarts.Name = "groupBoxStandarts";
            this.groupBoxStandarts.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxStandarts.Size = new System.Drawing.Size(912, 338);
            this.groupBoxStandarts.TabIndex = 4;
            this.groupBoxStandarts.TabStop = false;
            this.groupBoxStandarts.Text = "Список стандартов";
            // 
            // dataGridView_Standarts
            // 
            this.dataGridView_Standarts.AllowUserToAddRows = false;
            this.dataGridView_Standarts.AllowUserToDeleteRows = false;
            this.dataGridView_Standarts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Standarts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_Standarts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Standarts.Location = new System.Drawing.Point(8, 23);
            this.dataGridView_Standarts.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView_Standarts.Name = "dataGridView_Standarts";
            this.dataGridView_Standarts.RowHeadersVisible = false;
            this.dataGridView_Standarts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_Standarts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_Standarts.Size = new System.Drawing.Size(896, 308);
            this.dataGridView_Standarts.TabIndex = 1;
            // 
            // groupBoxStandartsSets
            // 
            this.groupBoxStandartsSets.Controls.Add(this.dataGridView_StandartsSet);
            this.groupBoxStandartsSets.Location = new System.Drawing.Point(8, 7);
            this.groupBoxStandartsSets.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxStandartsSets.Name = "groupBoxStandartsSets";
            this.groupBoxStandartsSets.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxStandartsSets.Size = new System.Drawing.Size(912, 338);
            this.groupBoxStandartsSets.TabIndex = 3;
            this.groupBoxStandartsSets.TabStop = false;
            this.groupBoxStandartsSets.Text = "Партии стандартов";
            // 
            // dataGridView_StandartsSet
            // 
            this.dataGridView_StandartsSet.AllowUserToAddRows = false;
            this.dataGridView_StandartsSet.AllowUserToDeleteRows = false;
            this.dataGridView_StandartsSet.AllowUserToOrderColumns = true;
            this.dataGridView_StandartsSet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_StandartsSet.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_StandartsSet.CausesValidation = false;
            this.dataGridView_StandartsSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_StandartsSet.Location = new System.Drawing.Point(8, 23);
            this.dataGridView_StandartsSet.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView_StandartsSet.MultiSelect = false;
            this.dataGridView_StandartsSet.Name = "dataGridView_StandartsSet";
            this.dataGridView_StandartsSet.ReadOnly = true;
            this.dataGridView_StandartsSet.RowHeadersVisible = false;
            this.dataGridView_StandartsSet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_StandartsSet.Size = new System.Drawing.Size(896, 308);
            this.dataGridView_StandartsSet.TabIndex = 1;
            this.dataGridView_StandartsSet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_StandartsSet_CellClick);
            // 
            // tabMonitors
            // 
            this.tabMonitors.Controls.Add(this.groupBoxMonitors);
            this.tabMonitors.Controls.Add(this.groupBoxMonitorsSets);
            this.tabMonitors.Location = new System.Drawing.Point(4, 24);
            this.tabMonitors.Margin = new System.Windows.Forms.Padding(4);
            this.tabMonitors.Name = "tabMonitors";
            this.tabMonitors.Padding = new System.Windows.Forms.Padding(4);
            this.tabMonitors.Size = new System.Drawing.Size(931, 703);
            this.tabMonitors.TabIndex = 2;
            this.tabMonitors.Text = "Мониторы";
            this.tabMonitors.UseVisualStyleBackColor = true;
            // 
            // groupBoxMonitors
            // 
            this.groupBoxMonitors.Controls.Add(this.dataGridView_Monitors);
            this.groupBoxMonitors.Location = new System.Drawing.Point(8, 353);
            this.groupBoxMonitors.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxMonitors.Name = "groupBoxMonitors";
            this.groupBoxMonitors.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxMonitors.Size = new System.Drawing.Size(912, 338);
            this.groupBoxMonitors.TabIndex = 4;
            this.groupBoxMonitors.TabStop = false;
            this.groupBoxMonitors.Text = "Список мониторов";
            // 
            // dataGridView_Monitors
            // 
            this.dataGridView_Monitors.AllowUserToAddRows = false;
            this.dataGridView_Monitors.AllowUserToDeleteRows = false;
            this.dataGridView_Monitors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Monitors.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_Monitors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Monitors.Location = new System.Drawing.Point(8, 23);
            this.dataGridView_Monitors.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView_Monitors.Name = "dataGridView_Monitors";
            this.dataGridView_Monitors.RowHeadersVisible = false;
            this.dataGridView_Monitors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView_Monitors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_Monitors.Size = new System.Drawing.Size(896, 308);
            this.dataGridView_Monitors.TabIndex = 1;
            // 
            // groupBoxMonitorsSets
            // 
            this.groupBoxMonitorsSets.Controls.Add(this.dataGridView_MonitorsSet);
            this.groupBoxMonitorsSets.Location = new System.Drawing.Point(8, 7);
            this.groupBoxMonitorsSets.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxMonitorsSets.Name = "groupBoxMonitorsSets";
            this.groupBoxMonitorsSets.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxMonitorsSets.Size = new System.Drawing.Size(912, 338);
            this.groupBoxMonitorsSets.TabIndex = 3;
            this.groupBoxMonitorsSets.TabStop = false;
            this.groupBoxMonitorsSets.Text = "Партии мониторов";
            // 
            // dataGridView_MonitorsSet
            // 
            this.dataGridView_MonitorsSet.AllowUserToAddRows = false;
            this.dataGridView_MonitorsSet.AllowUserToDeleteRows = false;
            this.dataGridView_MonitorsSet.AllowUserToOrderColumns = true;
            this.dataGridView_MonitorsSet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_MonitorsSet.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_MonitorsSet.CausesValidation = false;
            this.dataGridView_MonitorsSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_MonitorsSet.Location = new System.Drawing.Point(8, 23);
            this.dataGridView_MonitorsSet.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView_MonitorsSet.MultiSelect = false;
            this.dataGridView_MonitorsSet.Name = "dataGridView_MonitorsSet";
            this.dataGridView_MonitorsSet.ReadOnly = true;
            this.dataGridView_MonitorsSet.RowHeadersVisible = false;
            this.dataGridView_MonitorsSet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_MonitorsSet.Size = new System.Drawing.Size(896, 308);
            this.dataGridView_MonitorsSet.TabIndex = 1;
            this.dataGridView_MonitorsSet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_MonitorsSet_CellClick);
            // 
            // buttonReadFromFile
            // 
            this.buttonReadFromFile.Location = new System.Drawing.Point(963, 188);
            this.buttonReadFromFile.Margin = new System.Windows.Forms.Padding(4);
            this.buttonReadFromFile.Name = "buttonReadFromFile";
            this.buttonReadFromFile.Size = new System.Drawing.Size(141, 71);
            this.buttonReadFromFile.TabIndex = 5;
            this.buttonReadFromFile.Text = "Считать из файла";
            this.buttonReadFromFile.UseVisualStyleBackColor = true;
            this.buttonReadFromFile.Click += new System.EventHandler(this.buttonReadFromFile_Click);
            // 
            // buttonSave2File
            // 
            this.buttonSave2File.Location = new System.Drawing.Point(963, 309);
            this.buttonSave2File.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave2File.Name = "buttonSave2File";
            this.buttonSave2File.Size = new System.Drawing.Size(141, 71);
            this.buttonSave2File.TabIndex = 6;
            this.buttonSave2File.Text = "Сохранить в файл";
            this.buttonSave2File.UseVisualStyleBackColor = true;
            // 
            // buttonAddRow
            // 
            this.buttonAddRow.Location = new System.Drawing.Point(963, 430);
            this.buttonAddRow.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddRow.Name = "buttonAddRow";
            this.buttonAddRow.Size = new System.Drawing.Size(141, 71);
            this.buttonAddRow.TabIndex = 7;
            this.buttonAddRow.Text = "Добавить строку";
            this.buttonAddRow.UseVisualStyleBackColor = true;
            // 
            // buttonSave2DB
            // 
            this.buttonSave2DB.Location = new System.Drawing.Point(963, 556);
            this.buttonSave2DB.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave2DB.Name = "buttonSave2DB";
            this.buttonSave2DB.Size = new System.Drawing.Size(141, 71);
            this.buttonSave2DB.TabIndex = 8;
            this.buttonSave2DB.Text = "Сохранить данные в БД";
            this.buttonSave2DB.UseVisualStyleBackColor = true;
            // 
            // buttonReadWeight
            // 
            this.buttonReadWeight.Location = new System.Drawing.Point(963, 674);
            this.buttonReadWeight.Margin = new System.Windows.Forms.Padding(4);
            this.buttonReadWeight.Name = "buttonReadWeight";
            this.buttonReadWeight.Size = new System.Drawing.Size(141, 71);
            this.buttonReadWeight.TabIndex = 9;
            this.buttonReadWeight.Text = "Взвесить";
            this.buttonReadWeight.UseVisualStyleBackColor = true;
            // 
            // FaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 761);
            this.Controls.Add(this.buttonReadWeight);
            this.Controls.Add(this.buttonSave2DB);
            this.Controls.Add(this.buttonAddRow);
            this.Controls.Add(this.buttonSave2File);
            this.Controls.Add(this.buttonReadFromFile);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.groupBoxType);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FaceForm";
            this.ShowIcon = false;
            this.Text = "Взвешивание образцов";
            this.groupBoxType.ResumeLayout(false);
            this.groupBoxType.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.tabSamples.ResumeLayout(false);
            this.groupBoxSamples.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Samples)).EndInit();
            this.groupBoxSamplesSets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_SamplesSet)).EndInit();
            this.tabStandarts.ResumeLayout(false);
            this.groupBoxStandarts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Standarts)).EndInit();
            this.groupBoxStandartsSets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_StandartsSet)).EndInit();
            this.tabMonitors.ResumeLayout(false);
            this.groupBoxMonitors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Monitors)).EndInit();
            this.groupBoxMonitorsSets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_MonitorsSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxBothTypes;
        private System.Windows.Forms.CheckBox checkBoxSLI;
        private System.Windows.Forms.CheckBox checkBoxLLI;
        private System.Windows.Forms.GroupBox groupBoxType;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabSamples;
        private System.Windows.Forms.TabPage tabStandarts;
        private System.Windows.Forms.TabPage tabMonitors;
        private System.Windows.Forms.GroupBox groupBoxSamples;
        private System.Windows.Forms.DataGridView dataGridView_Samples;
        private System.Windows.Forms.GroupBox groupBoxSamplesSets;
        private System.Windows.Forms.DataGridView dataGridView_SamplesSet;
        private System.Windows.Forms.GroupBox groupBoxStandarts;
        private System.Windows.Forms.GroupBox groupBoxStandartsSets;
        private System.Windows.Forms.GroupBox groupBoxMonitors;
        private System.Windows.Forms.GroupBox groupBoxMonitorsSets;
        private System.Windows.Forms.Button buttonReadFromFile;
        private System.Windows.Forms.Button buttonSave2File;
        private System.Windows.Forms.Button buttonAddRow;
        private System.Windows.Forms.Button buttonSave2DB;
        private System.Windows.Forms.Button buttonReadWeight;
        private System.Windows.Forms.DataGridView dataGridView_StandartsSet;
        private System.Windows.Forms.DataGridView dataGridView_MonitorsSet;
        private System.Windows.Forms.DataGridView dataGridView_Standarts;
        private System.Windows.Forms.DataGridView dataGridView_Monitors;
    }
}
