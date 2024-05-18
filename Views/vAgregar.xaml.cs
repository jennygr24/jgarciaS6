using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace jgarciaS6.Views;



public partial class vAgregar : ContentPage
{
    public vAgregar()
    {
        InitializeComponent();
    }

    private async void txtGuardar_Clicked(object sender, EventArgs e)
    {
        try
        {
            
            var datos = new
            {
                nombre = txtNombre.Text,
                apellido = txtApellido.Text,
                edad = txtEdad.Text
            };

            
            string datosJson = JsonConvert.SerializeObject(datos);

            
            using (HttpClient cliente = new HttpClient())
            {
                
                string url = "http://localhost/moviles/wsestudiantes.php";

                
                var contenido = new StringContent(datosJson, Encoding.UTF8, "application/json");

                
                var respuesta = await cliente.PostAsync(url, contenido);

                
                if (respuesta.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Estudiante agregado correctamente", "OK");
                    
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Hubo un problema al agregar el estudiante", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}