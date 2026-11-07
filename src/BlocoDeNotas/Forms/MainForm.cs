using System.Drawing;
using System.Windows.Forms;

namespace Personal_Note_App.Forms;

public class Mainform : Form
{
    private readonly MenuStrip _menuStrip = new();
    private readonly ToolStrip _toolStrip = new();
    private readonly SplitContainer _splitContainer = new();

    private readonly ListBox _notesList = new();
    private readonly RichTextBox _editor = new();

    private readonly ToolStripStatusLabel _statusLabel = new();
    private readonly ToolStripStatusLabel _countLabel = new();

    public Mainform()
    {
        ConfigureWindow();
        ConfigureEditor();
        ConfigureMenu();
        ConfigureToolbar();
        ConfigureLayout();
        ConfigureStatusBar();

        UpdateStatus();
    }

    private void ConfigureWindow()
    {
        Text = "Personal Note App";

        Width = 1000;
        Height = 650;

        StartPosition = FormStartPosition.CenterScreen;

        MinimumSize = new Size(800, 500);
    }

    private void ConfigureEditor()
    {
        _editor.Dock = DockStyle.Fill;
        _editor.Font = new Font("Segoe UI", 11);
        _editor.BorderStyle = BorderStyle.None;
        _editor.AcceptsTab = true;
        _editor.DetectUrls = true;
        _editor.TextChanged += (_, _) => UpdateStatus();
    }

    private void ConfigureMenu()
    {
        ToolStripMenuItem fileMenu = new("Arquivo");

        fileMenu.DropDownItems.Add(
            new ToolStripMenuItem("Sair", null, (_, _) => Close())
        );

        ToolStripMenuItem editMenu = new("Editar");

        editMenu.DropDownItems.Add(
            new ToolStripMenuItem("Desfazer", null, (_, _) =>
            {
                if (_editor.CanUndo)
                _editor.Undo();
            })
        );

        editMenu.DropDownItems.Add(
            new ToolStripMenuItem("Refazer", null, (_,_) =>
            {
                if (_editor.CanRedo)
                _editor.Redo();
            })
        );

        editMenu.DropDownItems.Add(
            new ToolStripMenuItem("Selecionar tudo", null, (_, _) => 
                _editor.SelectAll())
        );

        ToolStripMenuItem formatMenu = new("Formatar");

        formatMenu.DropDownItems.Add(
            new ToolStripMenuItem("Escolher Fonte", null, (_, _) =>
                ChooseFont())
        );

        formatMenu.DropDownItems.Add(
            new ToolStripMenuItem("Cor do texto", null, (_, _) =>
                ChooseColor())
        );

        _menuStrip.Items.AddRange(
            new ToolStripItem[]
            {
                fileMenu,
                editMenu,
                formatMenu
            }
        );

        _menuStrip.Dock = DockStyle.Fill;

    }

    private void ConfigureToolbar()
    {
        ToolStripButton boldButton = new("B")
        {
            ToolTipText = "Negrito"
        };
        boldButton.Click += (_, _) => 
            ToggleFontStyle(FontStyle.Bold);

        ToolStripButton italicButton = new("I")
        {
            ToolTipText = "Itálico"
        };
        italicButton.Click += (_, _) =>
            ToggleFontStyle(FontStyle.Italic);

        ToolStripButton underlineButton = new("U")
        {
            ToolTipText = "Sublinhado"
        };
        underlineButton.Click += (_, _) =>
            ToggleFontStyle(FontStyle.Underline);

        ToolStripButton fontButton = new("Fonte");
        fontButton.Click += (_, _) => ChooseFont();
        
        ToolStripButton colorButton = new("Cor");
        colorButton.Click += (_, _) => ChooseColor();

        _toolStrip.Items.AddRange(
            new ToolStripItem[]
            {
                boldButton,
                italicButton,
                underlineButton,
                new ToolStripSeparator(),
                fontButton,
                colorButton
            }
        );

        _toolStrip.Dock = DockStyle.Fill;
    }

    private void ConfigureLayout()
    {
        TableLayoutPanel root = new()
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 4,
            Margin = Padding.Empty,
            Padding = Padding.Empty
        };

        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

       
        _splitContainer.Dock = DockStyle.Fill;

        _splitContainer.Orientation = Orientation.Vertical;

        TableLayoutPanel sidebar = new()
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(10)
        };

        sidebar.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        sidebar.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        Label notesTitle = new()
        {
            Text = "Minhas notas",
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            Dock = DockStyle.Fill,
            AutoSize = true,
            Padding = new Padding(0, 5, 0, 12)
        };

        _notesList.Dock = DockStyle.Fill;

        sidebar.Controls.Add(notesTitle, 0, 0);
        sidebar.Controls.Add(_notesList, 0, 1);

       
        _splitContainer.Panel1.Controls.Add(sidebar);

        _splitContainer.Panel2.Controls.Add(_editor);

        
        root.Controls.Add(_menuStrip, 0, 0);
        root.Controls.Add(_toolStrip, 0, 1);
        root.Controls.Add(_splitContainer, 0, 2);

        StatusStrip statusStrip = new();

        statusStrip.Items.Add(_statusLabel);
        statusStrip.Items.Add(new ToolStripStatusLabel { Spring = true });
        statusStrip.Items.Add(_countLabel);

        statusStrip.Dock = DockStyle.Fill;

        root.Controls.Add(statusStrip, 0, 3);

        Controls.Add(root);

        MainMenuStrip = _menuStrip;

        Shown += (_, _) =>
        {
            _splitContainer.Panel1MinSize = 180;
            _splitContainer.Panel2MinSize = 400;
            _splitContainer.SplitterDistance = 260;
        };
    }

    private void ConfigureStatusBar()
    {
        _statusLabel.Text = "Pronto";
    }

    private void UpdateStatus()
    {
        string[] words = _editor.Text.Split(
            (char[]?)null,
            StringSplitOptions.RemoveEmptyEntries
        );

        int characters = _editor.Text.Length;

        _countLabel.Text =
            $"{words.Length} palavras | {characters} caracteres";
    }

    private void ToggleFontStyle(FontStyle style)
    {
        Font currentFont = _editor.SelectionFont ?? _editor.Font;

        FontStyle newStyle = currentFont.Style ^ style;

        _editor.SelectionFont = new Font(currentFont, newStyle);
    }

    private void ChooseFont()
    {
        using FontDialog dialog = new();

        dialog.Font = _editor.SelectionFont ?? _editor.Font;

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _editor.SelectionFont = dialog.Font;
        }
    }

    private void ChooseColor()
    {
        using ColorDialog dialog = new();

        dialog.Color = _editor.SelectionColor;

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            _editor.SelectionColor = dialog.Color;
        }
    }
}