using jgarciaS6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace jgarciaS6.Views;
/*
public partial class vEstudiante : ContentPage
{
	private const string url = "http://192.168.56.1/moviles/wsestudiantes.php";
	private readonly HttpClient cliente = new HttpClient();
	private ObservableCollection<Estudiante> est;
	public vEstudiante()
	{
		InitializeComponent();
		ObtenerDatos();


    }
	public async void ObtenerDatos()
    {
		var content = await cliente.GetStringAsync(url);
		List<Estudiante> mostrar = JsonConvert.DeserializeObject<List<Estudiante>>(content);
		est = new ObservableCollection<Estudiante>(mostrar);
        ListaEstudiantes.ItemsSource = est;

        
        }
} */

public partial class vEstudiante : ContentPage
{
    private const string url = "http://192.168.1.22/moviles/wsestudiantes.php";
    private readonly HttpClient cliente = new HttpClient();
    public ObservableCollection<Estudiante> est { get; set; }

    public vEstudiante()
    {
        InitializeComponent();
        ObtenerDatos();
        BindingContext = this;     }

    public async void ObtenerDatos()
    {
        try
        {
            var content = await cliente.GetStringAsync(url);
            List<Estudiante> listaEstudiantes = JsonConvert.DeserializeObject<List<Estudiante>>(content);

            
            EstudiantesStackLayout.Children.Clear();

            
            int i = 0;
            while (i < listaEstudiantes.Count)
            {
                Estudiante estudiante = listaEstudiantes[i];

                
                Label lblCodigo = new Label { Text = estudiante.codigo.ToString(), WidthRequest = 80 };
                Label lblNombre = new Label { Text = estudiante.nombre, WidthRequest = 120 };
                Label lblApellido = new Label { Text = estudiante.apellido, WidthRequest = 120 };
                Label lblEdad = new Label { Text = estudiante.edad.ToString(), WidthRequest = 60 };

                
                EstudiantesStackLayout.Children.Add(new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { lblCodigo, lblNombre, lblApellido, lblEdad }
                });

                i++;
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
}