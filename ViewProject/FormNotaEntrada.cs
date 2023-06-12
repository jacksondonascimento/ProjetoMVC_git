﻿using ControllerProject;
using ModelProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewProject
{
    public partial class FormNotaEntrada : Form
    {
        private void ClearControlsNota()
        {
            dgvNotasEntrada.ClearSelection();
            dgvProdutos.ClearSelection();
            txtIDNotaEntrada.Text = string.Empty;
            cbxFornecedor.SelectedIndex = -1;
            txtNumero.Text = string.Empty;
            dtpEmissao.Value = DateTime.Now;
            dtpEntrada.Value = DateTime.Now;
            cbxFornecedor.Focus();
        }
        private void ChangeStatusOfControls(bool newStatus)
        {
            cbxProduto.Enabled = newStatus;
            txtCusto.Enabled = newStatus;
            txtQuantidade.Enabled = newStatus;
            BtnNovoProduto.Enabled = !newStatus;
            BtnGravarProduto.Enabled = newStatus;
            BtnCancelarProduto.Enabled = newStatus;
            BtnRemoverProduto.Enabled = newStatus;
        }

   
        private NotaEntradaController controller;
        private FornecedorController fornecedorController;
        private ProdutoController produtoController;
        private NotaEntrada notaAtual;
        public FormNotaEntrada(
        NotaEntradaController controller,
        FornecedorController
        fornecedorController,
        ProdutoController produtoController)
        {
            InitializeComponent();
            this.controller = controller;
            this.fornecedorController =
            fornecedorController;
            this.produtoController = produtoController;
            InicializaComboBoxs();
        }

        private void InicializaComboBoxs()
        {
            cbxFornecedor.Items.Clear();
            cbxProduto.Items.Clear();
            foreach (Fornecedor fornecedor in
            this.fornecedorController.GetAll())
            {
                cbxFornecedor.Items.Add(fornecedor);
            }
            foreach (Produto produto in
            this.produtoController.GetAll())
            {
                cbxProduto.Items.Add(produto);
            }
        }

        private void btnNovoNota_Click(object sender, EventArgs e)
        {
            ClearControlsNota();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            var notaEntrada = new NotaEntrada()
            {
                Id = (txtIDNotaEntrada.Text == string.Empty ? Guid.NewGuid() :new Guid(txtIDNotaEntrada.Text)),
                DataEmissao = dtpEmissao.Value,
                DataEntrada = dtpEntrada.Value,
                FornecedorNota = (Fornecedor)cbxFornecedor.SelectedItem,
                Numero = txtNumero.Text
            };
            notaEntrada = (txtIDNotaEntrada.Text == string.Empty ? this.controller.Insert(notaEntrada) : this.controller.Update(notaEntrada));
            dgvNotasEntrada.DataSource = null;
            dgvNotasEntrada.DataSource = this.controller.GetAll();
            ClearControlsNota();
        }

        private void btnCancelarNota_Click(object sender, EventArgs e)
        {
            ClearControlsNota();
        }

        private void btnRemoverNota_Click(object sender, EventArgs e)
        {
            if (txtIDNotaEntrada.Text == string.Empty)
            {
                MessageBox.Show(
                "Selecione a NOTA a ser removida no GRID");
            }
            else
            {
                this.controller.Remove(
                new NotaEntrada()
                {
                    Id = new Guid(txtIDNotaEntrada.Text)
                }
                );
                dgvNotasEntrada.DataSource = null;
                dgvNotasEntrada.DataSource =
                this.controller.GetAll();
                ClearControlsNota();
            }

        }

        private void dgvNotasEntrada_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.notaAtual = this.controller.
                GetNotaEntradaById((Guid)dgvNotasEntrada.
                CurrentRow.Cells[0].Value);
                txtIDNotaEntrada.Text = notaAtual.Id.
                ToString();
                txtNumero.Text = notaAtual.Numero;
                cbxFornecedor.SelectedItem = notaAtual.
                FornecedorNota;
                dtpEmissao.Value = notaAtual.DataEmissao;
                dtpEntrada.Value = notaAtual.DataEntrada;
               
            }
            catch (Exception){
                this.notaAtual = new NotaEntrada();
            }
        }

        private void cbxProduto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Produto_Click(object sender, EventArgs e)
        {
        
        }

        private void BtnGravarProduto_Click(object sender, EventArgs e)
        {
            var produtoNota = new ProdutoNotaEntrada()
            {
                Id = (txtIDProduto.Text == string.Empty ?Guid.NewGuid() : new Guid(txtIDProduto.Text)),
                PrecoCustoCompra = Convert.ToDouble(txtCusto.Text),
                ProdutoNota = (Produto)cbxProduto.SelectedItem,QuantidadeComprada = Convert.ToDouble(txtQuantidade.Text)
            };
            this.notaAtual.RegistrarProduto(produtoNota);
            this.notaAtual = this.controller.Update(
            this.notaAtual);
            ChangeStatusOfControls(false);
         
        }

        private void BtnCancelarProduto_Click(object sender, EventArgs e)
        {
           // ClearControlsProduto();
            ChangeStatusOfControls(false);
        }

        private void BtnRemoverProduto_Click(object sender, EventArgs e)
        {
            this.notaAtual.RemoverProduto(new ProdutoNotaEntrada()
            {
                Id = new Guid(txtIDProduto.Text)
            });
            this.controller.Update(this.notaAtual);
            ChangeStatusOfControls(false);

        }

        private void cbxFornecedor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormNotaEntrada_Load(object sender, EventArgs e)
        {

        }
    }
}
