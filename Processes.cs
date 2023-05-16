using System;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using Microsoft.Win32;
using Microsoft.VisualBasic;

namespace php_DN5
{
    public static class Processes
    {
        public static string[] file;
        public static List<int> row = new List<int>();
        public static string fileName;
        public static string filePath;
        public static string saveFileName;
        public static string startDistance;
        public static string direction;

        public static void openFile(){
            OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "PIM File(*.pim)|*.pim";
			if(openFileDialog.ShowDialog() == true)
                fileName = System.IO.Path.GetFileName(openFileDialog.FileName);
                filePath = openFileDialog.FileName;
            
            if(fileName != null){
                file = File.ReadAllLines(filePath);
            }

            ((MainWindow)System.Windows.Application.Current.MainWindow).fileName.Content = ("Filename: "  + fileName);
            ((MainWindow)System.Windows.Application.Current.MainWindow).openFile.Visibility = Visibility.Collapsed;
            ((MainWindow)System.Windows.Application.Current.MainWindow).saveFile.Visibility = Visibility.Visible;

            for (int i = 0; i < file.Length; i++) 
                {
                   if(file[i] == "G0Z15"){
                        row.Add(i+1);
                   }  
                }  

            ((MainWindow)System.Windows.Application.Current.MainWindow).saveFile.IsEnabled = true;

        }

        public static void saveFile(){

            startDistance = Interaction.InputBox("Enter New Position","","");

            if (((MainWindow)System.Windows.Application.Current.MainWindow).xProfile.IsChecked == true){
                direction = "Y";
            } else{
                direction = "X";
            }

            foreach(int i in row){
                file[i] = (direction + startDistance);
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PIM File(*.pim)|*.pim";

            if(saveFileDialog.ShowDialog() == true)
                saveFileName = saveFileDialog.FileName;

                if(saveFileName != null)  {

            
                File.WriteAllText(saveFileName, String.Empty);

                StreamWriter streamWriter = File.AppendText(saveFileName);
                foreach(string value in file)
                        streamWriter.WriteLine(value);
                streamWriter.Flush();
                streamWriter.Close();}

        }
    }
}