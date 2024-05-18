using jgarciaS6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace jgarciaS6.Views;

public partial class vEstudiante : ContentPage
{
    private const string url = "http://192.168.1.22/moviles/wsestudiantes.php";
    private readonly HttpClient cliente = new HttpClient();
    public ObservableCollection<Estudiante> est { get; set; }

    public vEstudiante()
    {
        InitializeComponent();
        ObtenerDatos();
        BindingContext = this;
    }

    public async void ObtenerDatos()
    {
        try
        {
            var content = await cliente.GetStringAsync(url);
            List<Estudiante> listaEstudiantes = JsonConvert.DeserializeObject<List<Estudiante>>(content);

            EstudiantesStackLayout.Children.Clear();

            foreach (var estudiante in listaEstudiantes)
            {
                var stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                        {
                            new Label { Text = estudiante.codigo.ToString(), WidthRequest = 80 },
                            new Label { Text = estudiante.nombre, WidthRequest = 120 },
                            new Label { Text = estudiante.apellido, WidthRequest = 120 },
                            new Label { Text = estudiante.edad.ToString(), WidthRequest = 60 }
                        }
                };

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) => OnEstudianteTapped(estudiante);
                stackLayout.GestureRecognizers.Add(tapGestureRecognizer);

                EstudiantesStackLayout.Children.Add(stackLayout);
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error de solicitud HTTP: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error de deserialización JSON: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async void OnEstudianteTapped(Estudiante estudiante)
    {
        var actualizarPage = new vActEliminar(estudiante);
        actualizarPage.Disappearing += (sender, e) => ObtenerDatos();
        await Navigation.PushAsync(actualizarPage);
    }

    private async void btnAgregarEstudiante_Clicked(object sender, EventArgs e)
    {
        var agregarPage = new vAgregar();
        agregarPage.Disappearing += (sender, e) => ObtenerDatos();
        await Navigation.PushAsync(agregarPage);
    }
}