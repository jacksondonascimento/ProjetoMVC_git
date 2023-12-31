﻿using ControllerProject;
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
    public partial class FormJanelaPrincipal : Form
    {
        private FornecedorController fornecedorController = new FornecedorController();
        private ProdutoController produtoController = new ProdutoController();
        public FormJanelaPrincipal()
        {
            InitializeComponent();
        }

        private void fornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormFornecedor(fornecedorController).ShowDialog();

        }
         
        private void compraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotaEntradaController controller = null;
            new FormNotaEntrada(controller, fornecedorController, produtoController).ShowDialog();

        }

        private void FormJanelaPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
