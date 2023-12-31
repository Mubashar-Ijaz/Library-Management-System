﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace F200220__Mini_Project
{
    public partial class read_Form : Form
    {
        public class Book
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string ISBN { get; set; }
            public int Copies { get; set; }
        }

        private List<Book> books; // Store the books data

        public read_Form()
        {
            InitializeComponent();
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void home_label_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void Insert_label_Click(object sender, EventArgs e)
        {
            this.Hide();
            insert_Form f2 = new insert_Form();
            f2.ShowDialog();
        }

        private void view_label_Click(object sender, EventArgs e)
        {
            this.Hide();
            read_Form f3 = new read_Form();
            f3.ShowDialog();
        }

        private void read_Form_Load(object sender, EventArgs e)
        {
            // Set the folder path
            string folderPath = @"D:\F200220, Mini Project\F200220, Mini Project\bin\Library System";

            // Create a list to hold the book data
            books = new List<Book>();

            // Retrieve the file names in the folder
            string[] fileNames = Directory.GetFiles(folderPath);

            // Read data from each file and create Book objects
            foreach (string fileName in fileNames)
            {
                string[] lines = File.ReadAllLines(fileName);

                // Check if the file contains at least 1 line (minimum required for a book entry)
                if (lines.Length >= 1)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] data = lines[i].Split(',');

                        // Check if the data array has 4 elements (minimum required for a book entry)
                        if (data.Length >= 4)
                        {
                            Book book = new Book
                            {
                                Title = data[0].Trim(),
                                Author = data[1].Trim(),
                                ISBN = data[2].Trim(),
                                Copies = int.Parse(data[3].Trim())
                            };

                            books.Add(book);
                        }
                    }
                }
            }

            // Bind the list of books to the DataGridView
            dataGridView1.DataSource = books;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchTerm = BookName_textBox.Text.ToLower();
            bool bookFound = false;

            // Iterate through the books and search for the matching title
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Book book = (Book)row.DataBoundItem;
                if (book.Title.ToLower().Contains(searchTerm))
                {
                    // Select the matching row and scroll it into view
                    row.Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                    bookFound = true;

                    // Display the book data in the textboxes
                    textBox1.Text = book.Title;
                    textBox2.Text = book.Author;
                    textBox3.Text = book.ISBN;
                    textBox4.Text = book.Copies.ToString();

                    break;
                }
            }

            if (!bookFound)
            {
                MessageBox.Show("Book is not available.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void update_label_Click(object sender, EventArgs e)
        {
            this.Hide();
            Update_Form f4 = new Update_Form();
            f4.ShowDialog();
        }

        private void delete_label_Click(object sender, EventArgs e)
        {
            this.Hide();
            delete_Form f4 = new delete_Form();
            f4.ShowDialog();
        }
    }
}
