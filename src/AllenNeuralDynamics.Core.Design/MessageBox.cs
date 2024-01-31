using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

[Combinator]
[Description("Creates a custom message box dialog window.")]
[WorkflowElementCategory(ElementCategory.Sink)]
public class MessageBox
{
    private string text = "";
    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    private string title = "Warning";
    public string Title
    {
        get { return title; }
        set { title = value; }
    }


    private MessageBoxIcon messageBoxIcon = MessageBoxIcon.Warning;
    public MessageBoxIcon MessageBoxIcon
    {
        get { return messageBoxIcon; }
        set { messageBoxIcon = value; }
    }


    public IObservable<TSource> Process<TSource>(IObservable<TSource> source)
    {
        return source.Select(value =>
        {
            var task = new Task(() => System.Windows.Forms.MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon));
            task.Start();
            return value;
        });
    }
}
