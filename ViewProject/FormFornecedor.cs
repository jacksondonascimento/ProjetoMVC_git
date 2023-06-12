﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControllerProject;
using ModelProject;

namespace ViewProject
{
    public partial class FormFornecedor : Form
    {
        private FornecedorController controller;


        public FormFornecedor(FornecedorController controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            var fornecedor = new Fornecedor()
            {
                Id = (txtId.Text == string.Empty ?
                    Guid.NewGuid() : new Guid(txtId.Text)),
                Nome = txtNome.Text,
                CNPJ = txtCnpj.Text
            };

            fornecedor = (txtId.Text == string.Empty ?
                    this.controller.Insert(fornecedor) :
                    this.controller.Update(fornecedor));
            dgvFornecedor.DataSource = null;
            dgvFornecedor.DataSource = this.controller.GetAll();
            ClearControls();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void dgvFornecedor_SelectionChanged(object sender, EventArgs e)
        {
            dgvFornecedor.ClearSelection();
            
            txtId.Text = dgvFornecedor.CurrentRow.Cells[0].
                Value.ToString();
            txtNome.Text = dgvFornecedor.CurrentRow.Cells[1].
                Value.ToString();
            txtCnpj.Text = dgvFornecedor.CurrentRow.Cells[2].
                Value.ToString();
            
        }

        private void ClearControls()
        {
            dgvFornecedor.ClearSelection();
            txtId.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtCnpj.Text = string.Empty;
            txtNome.Focus();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (txtId.Text == string.Empty)
                MessageBox.Show(
                    "Selecione o FORNECEDOR a ser removido " +
                    "do GRID");
            else
            {
                this.controller.Remove(
                    new Fornecedor()
                    {
                        Id = new Guid(txtId.Text)
                    });

                dgvFornecedor.DataSource = null;
                dgvFornecedor.DataSource =
                    this.controller.GetAll();
                ClearControls();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblId_Click(object sender, EventArgs e)
        {

        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblNome_Click(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCnpj_Click(object sender, EventArgs e)
        {

        }

        private void txtCnpj_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void dgvFornecedor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
