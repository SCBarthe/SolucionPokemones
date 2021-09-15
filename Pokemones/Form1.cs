using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Windows.Forms;
using modelo;
using negocio;
using System.IO; 



namespace Pokemones
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dgvPokemon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar(); 

        }

        private void dgvPokemons_SelectionChanged(DataObjectMethodType sender, EventArgs e)
        {
            Pokemon poke = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            pbxPokemon.Load(poke.UrlImagen); 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmPokemon ventanaNuevo = new frmPokemon();
            ventanaNuevo.ShowDialog();
            cargar();
        }

        private void pbxPokemon_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Pokemon poke = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
                pbxPokemon.Load(poke.UrlImagen);
            }
            catch (FileNotFoundException ex)
            {
                pbxPokemon.Load("");
            }
        }

        private void cargar()
        {

            //Cargar planilla con pokemon desde la DB
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                List<Pokemon> listaObtenida = negocio.listar();

                dgvPokemons.DataSource = listaObtenida;
                dgvPokemons.Columns[0].Visible = false;
                dgvPokemons.Columns[4].Visible = false;

                pbxPokemon.Load(listaObtenida[0].UrlImagen);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem; 
            frmPokemon ventanaNuevo = new frmPokemon(seleccionado);
            ventanaNuevo.ShowDialog();
            cargar();


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();

            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;

            try
            {
                if(MessageBox.Show("Se eleminara permanentemente", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)


                negocio.eliminarFisico(seleccionado.Id);

                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar" + ex.ToString()); 

            }

        }

        private void btnEliminarDos_Click(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();

            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;

            try
            {
                if (MessageBox.Show("Se eleminara permanentemente", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)


                    negocio.eliminarLogico(seleccionado.Id);

                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar" + ex.ToString());

            }



        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {

            string filtro = txtFiltro.Text; 

        }
    }
}
