using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Subject sub;
        List<Subject> lsSub = new List<Subject>();

        public void saveAll()
        {
            foreach (Subject sub in lsSub)
            {
                StreamWriter tw = File.CreateText($"{sub.name}.txt");
                string[] reviseLines = sub.revise.Split('\n');
                foreach (string line in reviseLines)
                {
                    tw.WriteLine(sub.revise);
                    if (line.Split(':') == { })
                    try
                    {
                        double check = Convert.ToDouble(line.Split(':')[0]);
                        DateTime dt = line;
                        tw.WriteLine(line.ToBinary());
                    }
                    catch (InvalidCastException ex)
                    {
                        tw.WriteLine(sub.revise);
                    }
                }
            }
        }


        private void cmbSub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cmbSub.SelectedValue;
            sub = new Subject(item.Content.ToString());
            lsSub.Add(sub);
        }
        
        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            LectureNote note = new LectureNote(DateTime.Now, txtNote.Text);
            sub.addNote(note);
        }

        private void btnDefAdd_Click(object sender, RoutedEventArgs e)
        {
            Definition def = new Definition(txtTerm.Text, txtDef.Text);
            sub.addDef(def);
        }

        private void btnExAdd_Click(object sender, RoutedEventArgs e)
        {
            Example ex = new Example(txtConcept.Text, txtEx.Text);
            sub.addEx(ex);
        }

        private void btnRev_Click(object sender, RoutedEventArgs e)
        {
            revise()
        }

        public void revise()
        {
            txtRevise.Text = sub.name.ToString() + ": \n";
            txtRevise.Text += "Definitions: \n";
            foreach (Definition def in sub.myDefs)
            {
                txtRevise.Text += def.ToString() + "\n";
            }
            txtRevise.Text += "Examples: \n";
            foreach (Example ex in sub.myExs)
            {
                txtRevise.Text += ex.ToString() + "\n";
            }
            txtRevise.Text += "Lecture Notes: \n";
            foreach (LectureNote note in sub.myLNs)
            {
                txtRevise.Text += note.ToString() + "\n";
            }
            sub.revise = txtRevise.Text;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            saveAll();
        }
    }

    public class Subject
    {
        public string name;
        public string revise;
        public List<Definition> myDefs = new List<Definition>();
        public List<Example> myExs = new List<Example>();
        public List<LectureNote> myLNs = new List<LectureNote>();
        
        public Subject(string name)
        {
            this.name = name;
        }

        public void addDef(Definition myDef)
        {
            myDefs.Add(myDef);
        }

        public void addEx(Example myEx)
        {
            myExs.Add(myEx);
        }

        public void addNote(LectureNote myLN)
        {
            myLNs.Add(myLN);
        }

        public void save()
        {
            TextWriter tw = File.CreateText($"{name}.txt");
            //// Notes
            //tw.WriteLine($"{name} Notes:");
            //foreach (LectureNote note in myLNs)
            //{
            //    tw.WriteLine(note.ToString());
            //}
            //// Definitons
            //tw.WriteLine($"{name} Notes:");
            //foreach (Definition def in myDefs)
            //{
            //    tw.WriteLine(def.ToString());
            //}
            //// Examples
            //tw.WriteLine($"{name} Notes:");
            //foreach (Definition def in myDefs)
            //{
            //    tw.WriteLine(def.ToString());
            //}
        }
    }

    public class LearningItem
    {
        string text;
        public LearningItem(string textbx)
        {
            text = textbx;
        }
    }

    public class Definition : LearningItem
    {
        string term;
        string def;
        
        public Definition(string term, string text):base(text)
        {
            this.term = term;
            def = text;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", term, def);
        }
    }

    public class Example : LearningItem
    {
        string concept;
        string Ex;

        public Example(string concept, string text) : base(text)
        {
            this.concept = concept;
            Ex = text;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", concept, Ex);
        }
    }

    public class LectureNote : LearningItem
    {
        DateTime lecDate;
        string notes;
        
        public LectureNote(DateTime date, string text) : base(text)
        {
            lecDate = date;
            notes = text;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", lecDate.ToShortDateString(), notes);
        }
    }
}
