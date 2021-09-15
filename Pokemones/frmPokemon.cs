﻿using System;
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

namespace Pokemones
{
    public partial class frmPokemon : Form
    {
        private Pokemon pokemon = null;

        public frmPokemon()
        {
            InitializeComponent();
        }

        public frmPokemon(Pokemon pokemon)
        {
            InitializeComponent();
            this.pokemon = pokemon; 
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Pokemon nuevo = new Pokemon();
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                if (pokemon == null)
                    pokemon = new Pokemon(); 

                pokemon.Numero = (int)numNumero.Value;
                pokemon.Nombre = txtNombre.Text;
                pokemon.Descripcion = txtDescripcion.Text;
                pokemon.UrlImagen = txtUrlImagen.Text;

                nuevo.Tipo = (Elemento)cboTipo.SelectedItem;
                nuevo.Debilidad = (Elemento)cboDebilidad.SelectedItem; 
                
                if(pokemon.Id != 0)
                {
                    negocio.modificar(pokemon);
                    MessageBox.Show("pokemon a sido modificado");

                }
                else
                {
                    negocio.agregar(pokemon);
                    MessageBox.Show("Pokemon agregado"); 

                }
               

                
                Close(); 



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tbxNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmPokemon_Load(object sender, EventArgs e)
        {
            ElementoNegocio elementoNegocio = new ElementoNegocio();
            try
            {
                cboTipo.DataSource = elementoNegocio.listar();
                cboTipo.ValueMember = "Id";
                cboTipo.DisplayMember = "Descripcion"; 


                cboDebilidad.DataSource = elementoNegocio.listar();
                cboDebilidad.ValueMember = "Id";
                cboDebilidad.DisplayMember = "Descripcion"; 


                if (pokemon != null)
                {
                    
                    
                    numNumero.Value = pokemon.Numero;
                    txtNombre.Text = pokemon.Nombre;
                    txtDescripcion.Text = pokemon.Descripcion;
                    txtUrlImagen.Text = pokemon.UrlImagen;
                    cboTipo.SelectedValue = pokemon.Tipo.Id;
                    cboDebilidad.SelectedValue = pokemon.Debilidad.Id; 



                }
                 


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }
        }




    }
}
