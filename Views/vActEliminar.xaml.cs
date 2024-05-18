using jgarciaS6.Models;
using Newtonsoft.Json;
using System.Text;

namespace jgarciaS6.Views;


public partial class vActEliminar : ContentPage
{
    private Estudiante estudiante;

    public vActEliminar(Estudiante estudiante)
    {
        InitializeComponent();
        this.estudiante = estudiante;
        BindingContext = this.estudiante;
        LlenarDatos();
    }

    private void LlenarDatos()
    {
        txtCodigo.Text = estudiante.codigo.ToString();
        txtNombre.Text = estudiante.nombre;
        txtApellido.Text = estudiante.apellido;
        txtEdad.Text = estudiante.edad.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        estudiante.nombre = txtNombre.Text;
        estudiante.apellido = txtApellido.Text;
        estudiante.edad = int.Parse(txtEdad.Text);

        var json = JsonConvert.SerializeObject(estudiante);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await new HttpClient().PutAsync($"http://localhost/moviles/wsestudiantes.php?codigo={estudiante.codigo}", content);

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Éxito", "Estudiante actualizado correctamente", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Hubo un problema al actualizar el estudiante", "OK");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var response = await new HttpClient().DeleteAsync($"http://localhost/moviles/wsestudiantes.php?codigo={estudiante.codigo}");

        if (response.IsSuccessStatusCode)
        {
            await DisplayAlert("Éxito", "Estudiante eliminado correctamente", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Hubo un problema al eliminar el estudiante", "OK");
        }
    }
}