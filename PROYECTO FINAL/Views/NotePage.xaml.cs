namespace MAUI.Notes.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class NotePage : ContentPage
{
	public string ItemId { set { LoadNote(value); } }
	public NotePage()
	{
		InitializeComponent();

		string appDataPath = FileSystem.AppDataDirectory;
		string randomFile = $"{Path.GetRandomFileName()}.notes.txt";

		LoadNote(Path.Combine(appDataPath, randomFile));
	}
	private void LoadNote(string fileName)
	{
		Models.Note note = new Models.Note();
		note.FileName = fileName;

		if(File.Exists(note.FileName))
		{
			note.Date = File.GetCreationTime(fileName);
			note.Text = File.ReadAllText(fileName);
		}

		BindingContext = note;
	}

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
		if(BindingContext is Models.Note note)
			File.WriteAllText(note.FileName, TextEditor.Text);
        await Shell.Current.GoToAsync("..");

    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
		{
            //Delete the file

            if (File.Exists(note.FileName))
                File.Delete(note.FileName);
        }

		await Shell.Current.GoToAsync("..");
    }
}