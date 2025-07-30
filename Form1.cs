using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PDV_Bug_Example
{
    public partial class Form1 : Form
    {
        private List<string> itensVenda = new List<string>();

        public Form1()
        {
            InitializeComponent();
            // Inicializa a lista de produtos dispon�veis
            lstProdutosDisponiveis.Items.Add("Produto A");
            lstProdutosDisponiveis.Items.Add("Produto B");
            lstProdutosDisponiveis.Items.Add("Produto C");
            lstProdutosDisponiveis.Items.Add("Produto D");
            lstProdutosDisponiveis.Items.Add("Produto E");
            lstProdutosDisponiveis.Items.Add("Produto F");
            lstProdutosDisponiveis.Items.Add("Produto G");
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (lstProdutosDisponiveis.SelectedItem != null)
            {
                string produtoSelecionado = lstProdutosDisponiveis.SelectedItem.ToString();
                itensVenda.Add(produtoSelecionado);
                AtualizarCarrinho();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um produto para adicionar.", "Aviso");
            }
        }

        private void AtualizarCarrinho()
        {
            lstCarrinho.Items.Clear();
            foreach (var item in itensVenda)
            {
                lstCarrinho.Items.Add(item);
            }
            lblTotalItens.Text = $"Total de Itens: {itensVenda.Count}";
        }


        private void btnFinalizarVenda_Click(object sender, EventArgs e)
        {
            if (itensVenda.Count == 0)
            {
                MessageBox.Show("O carrinho est� vazio!", "Erro");
                return;
            }

            // O erro est� aqui: o array foi inicializado com um tamanho fixo de 5.
            // Se 'itensVenda' tiver 6 ou mais itens, ocorrer� um IndexOutOfRangeException.

            //tamanho do array corrigido 
            string[] itensParaProcessar = new string[itensVenda.Count];

            try
            {
                for (int i = 0; i < itensVenda.Count; i++)
                {
                    // A exce��o ocorrer� aqui quando 'i' for 5.
                    itensParaProcessar[i] = itensVenda[i].ToUpper();
                    Console.WriteLine($"Processando: {itensParaProcessar[i]}");
                }

                MessageBox.Show("Venda finalizada com sucesso!", "Sucesso");
                itensVenda.Clear();
                AtualizarCarrinho();
            }
            catch (IndexOutOfRangeException ex)
            {
                // Em um cen�rio real, o debugger do Visual Studio pausaria na linha do erro
                // antes mesmo de chegar no catch, se n�o estiver dentro de um try-catch.
                MessageBox.Show(
                    "Ocorreu um erro cr�tico ao processar a venda!\n\n" +
                    "Detalhes para o desenvolvedor:\n" +
                    $"Erro: {ex.Message}\n" +
                    "O sistema tentou acessar uma posi��o de mem�ria inv�lida. Verifique o tamanho do array 'itensParaProcessar'.",
                    "ERRO FATAL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
        
