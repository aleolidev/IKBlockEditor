using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        //TILESET PRIMARIO

        string completeValue = null; //Value of metatiles.bin, principal tileset
        string ultraString = ""; //String of richTextBox for metatiles.bin, principal tileset
        string completeValueAttribute = null; //Value of metatiles_attributes.bin, principal tileset
        string ultraStringAttribute = ""; //String of richTextBox for metatiles_attribute.bin, principal tileset
        int bytesToLoad = 0; //Bytes to load from metatiles.bin, principal tileset
        int bytesToLoadAttribute = 0; //Bytes to load from metatiles_attributes.bin, principal tileset
        string binFile = ""; //Path of metatiles.bin, principal tileset
        string binAttributeFile = ""; //Path of metatiles_attributes.bin, principal tileset
        string pngFile = ""; //Path of tiles.bmp, principal tileset
        string dirPalettes = null; //Palette directory, principal tileset
        string directory = null; //Directory of principal tileset
        ColorPalette[] palettes = new ColorPalette[16];//Palettes of principal tileset

        //TILESET SECUNDARIO

        string tl2CompleteValue = null; //Value of metatiles.bin, secondary tileset - not used
        string tl2UltraString = ""; //String of richTextBox for metatiles.bin, secondary tileset
        string tl2CompleteValueAttribute = null; //Value of metatiles_attributes.bin, secondary tileset - not used
        string tl2UltraStringAttribute = ""; //String of richTextBox for metatiles_attribute.bin, secondary tileset
        long tl2BytesToLoad = 0; //Bytes to load from metatiles.bin, secondary tileset
        long tl2BytesToLoadAttribute = 0; //Bytes to load from metatiles_attributes.bin, secondary tileset
        string tl2BinFile = ""; //Path of metatiles.bin, secondary tileset
        string tl2BinAttributeFile = ""; //Path of metatiles_attributes.bin, secondary tileset
        string tl2PngFile = ""; //Path of tiles.bmp, secondary tileset
        string tl2DirPalettes = null; //Palette directory, secondary tileset
        string tl2Directory = null; //Directory of secondary tileset

        //GENERIC VARS

        int blockCounter = 0; //Amount of tiles at right side
        int tileBlockCounter = 0; //Amount of tileBlocks at left side
        int blockPicked = 0; //Tile picked from right
        string blockSelected = ""; //Tileblock picked from left
        Image tileset; //Image of tileset
        Image tl2Tileset; //Image of tileset
        bool xFlipped = false; //Bool to set if is xFlipped or not
        bool yFlipped = false; //Bool to set if is yFlipped or not
        int selectedPalette = 0; //Palette selected
        int palettesLoaded = 0; //Total palettes loaded

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void reloadFile() {
            ultraString = "";
            completeValue = "";

            BinaryReader br = new BinaryReader(File.OpenRead(binFile));

            string value = null;
            int lineCounter = 0;
            int totalCounter = 0;
            var offset = "";

            ultraString += "00000000: ";
            for (int i = 0; i < bytesToLoad; i++)
            {
                br.BaseStream.Position = i;
                value = br.ReadByte().ToString("X");
                if (value.Length == 1)
                {
                    value = "0" + value;
                }
                ultraString += value;
                completeValue += value;
                lineCounter += 2;
                totalCounter += 2;
                if (lineCounter == 32 && totalCounter / 2 < bytesToLoad)
                {
                    offset = (i + 1).ToString("X");
                    ultraString += Environment.NewLine;
                    switch (offset.Length)
                    {
                        case 2:
                            offset = "000000" + offset;
                            break;
                        case 3:
                            offset = "00000" + offset;
                            break;
                        case 4:
                            offset = "0000" + offset;
                            break;
                        case 5:
                            offset = "000" + offset;
                            break;
                        case 6:
                            offset = "00" + offset;
                            break;
                        case 7:
                            offset = "0" + offset;
                            break;
                    }
                    ultraString += offset + ": ";
                    lineCounter = 0;
                }
                else
                {
                    ultraString += " ";
                }
            }
            //MessageBox.Show(totalCounter.ToString());
            richTextBox1.Text = ultraString;
            br.Close();


            //READ TILESET2

            
            if (tl2BinFile != "") { 
                BinaryReader tl2Br = new BinaryReader(File.OpenRead(tl2BinFile));
                tl2UltraString += "00000000: ";
                offset = "";
                lineCounter = 0;
                for (int i = 0; i < tl2BytesToLoad; i++)
                {
                    tl2Br.BaseStream.Position = i;
                    value = tl2Br.ReadByte().ToString("X");
                    if (value.Length == 1)
                    {
                        value = "0" + value;
                    }
                    tl2UltraString += value;
                    completeValue += value;
                    lineCounter += 2;
                    totalCounter += 2;
                    //MessageBox.Show(totalCounter.ToString());
                    if ((lineCounter == 32) && ((totalCounter / 2) < (tl2BytesToLoad + bytesToLoad)))
                    {
                        offset = (i + 1).ToString("X");
                        tl2UltraString += Environment.NewLine;
                        switch (offset.Length)
                        {
                            case 2:
                                offset = "000000" + offset;
                                break;
                            case 3:
                                offset = "00000" + offset;
                                break;
                            case 4:
                                offset = "0000" + offset;
                                break;
                            case 5:
                                offset = "000" + offset;
                                break;
                            case 6:
                                offset = "00" + offset;
                                break;
                            case 7:
                                offset = "0" + offset;
                                break;
                        }
                        tl2UltraString += offset + ": ";
                        lineCounter = 0;
                    }
                    else
                    {
                        tl2UltraString += " ";
                    }
                }
                richTextBox3.Text = tl2UltraString;
                tl2Br.Close();
            }
        }

        private void reloadFileAttribute()
        {
            ultraStringAttribute = "";
            completeValueAttribute = "";

            BinaryReader br = new BinaryReader(File.OpenRead(binAttributeFile));

            string value = null;
            int lineCounter = 0;
            int totalCounter = 0;
            var offset = "";

            ultraStringAttribute += "00000000: ";
            for (int i = 0; i < bytesToLoadAttribute; i++)
            {
                br.BaseStream.Position = i;
                value = br.ReadByte().ToString("X");
                if (value.Length == 1)
                {
                    value = "0" + value;
                }
                ultraStringAttribute += value;
                completeValueAttribute += value;
                lineCounter += 2;
                totalCounter += 2;
                if (lineCounter == 32 && totalCounter / 2 < bytesToLoadAttribute)
                {
                    offset = (i + 1).ToString("X");
                    ultraStringAttribute += Environment.NewLine;
                    switch (offset.Length)
                    {
                        case 2:
                            offset = "000000" + offset;
                            break;
                        case 3:
                            offset = "00000" + offset;
                            break;
                        case 4:
                            offset = "0000" + offset;
                            break;
                        case 5:
                            offset = "000" + offset;
                            break;
                        case 6:
                            offset = "00" + offset;
                            break;
                        case 7:
                            offset = "0" + offset;
                            break;
                    }
                    ultraStringAttribute += offset + ": ";
                    lineCounter = 0;
                }
                else
                {
                    ultraStringAttribute += " ";
                }
            }
            richTextBox2.Text = ultraStringAttribute;
            br.Close();

            // TILESET 2 
            if (tl2BinAttributeFile != "")
            {
                BinaryReader tl2Br = new BinaryReader(File.OpenRead(tl2BinAttributeFile));
                tl2UltraStringAttribute += "00000000: ";
                lineCounter = 0;
                for (int i = 0; i < tl2BytesToLoadAttribute; i++)
                {
                    tl2Br.BaseStream.Position = i;
                    value = tl2Br.ReadByte().ToString("X");
                    if (value.Length == 1)
                    {
                        value = "0" + value;
                    }
                    tl2UltraStringAttribute += value;
                    completeValueAttribute += value;
                    lineCounter += 2;
                    totalCounter += 2;
                    if ((lineCounter == 32) && ((totalCounter / 2) < (bytesToLoadAttribute + tl2BytesToLoadAttribute)))
                    {
                        offset = (i + 1).ToString("X");
                        tl2UltraStringAttribute += Environment.NewLine;
                        switch (offset.Length)
                        {
                            case 2:
                                offset = "000000" + offset;
                                break;
                            case 3:
                                offset = "00000" + offset;
                                break;
                            case 4:
                                offset = "0000" + offset;
                                break;
                            case 5:
                                offset = "000" + offset;
                                break;
                            case 6:
                                offset = "00" + offset;
                                break;
                            case 7:
                                offset = "0" + offset;
                                break;
                        }
                        tl2UltraStringAttribute += offset + ": ";
                        lineCounter = 0;
                    }
                    else
                    {
                        tl2UltraStringAttribute += " ";
                    }
                }
                richTextBox4.Text = tl2UltraStringAttribute;
                tl2Br.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "") { 
                int pos = int.Parse(textBox4.Text, System.Globalization.NumberStyles.HexNumber);
                var stringAmount = textBox3.Text.ToString();
                string finalValue = null;

                //MessageBox.Show(comboBox4.SelectedIndex.ToString());
                //BinaryReader br = new BinaryReader(File.OpenRead(binFile));

                switch (comboBox4.SelectedIndex)
                {
                    case 0:
                        BinaryReader br = new BinaryReader(File.OpenRead(binFile));
                        var pr = "";

                        switch (stringAmount.Length)
                        {
                            case 2:

                                for (int i = 1; i <= 3; i++)
                                {
                                    br.BaseStream.Position = pos + i;
                                    pr = br.ReadByte().ToString("X");
                                    if (pr.Length == 1)
                                    {
                                        pr = "0" + pr;
                                        finalValue += pr;
                                    }
                                    else
                                    {
                                        finalValue += pr;
                                    }
                                }
                                finalValue = textBox3.Text + finalValue;
                                break;
                            case 4:

                                for (int i = 2; i <= 3; i++)
                                {
                                    br.BaseStream.Position = pos + i;
                                    pr = br.ReadByte().ToString("X");
                                    if (pr.Length == 1)
                                    {
                                        pr = "0" + pr;
                                        finalValue += pr;
                                    }
                                    else
                                    {
                                        finalValue += pr;
                                    }
                                }
                                finalValue = textBox3.Text + finalValue;
                                break;
                            case 6:
                                br.BaseStream.Position = pos + 3;
                                pr = br.ReadByte().ToString("X");
                                if (pr.Length == 1)
                                {
                                    pr = "0" + pr;
                                    finalValue += pr;
                                }
                                else
                                {
                                    finalValue += pr;
                                }
                                finalValue = textBox3.Text + finalValue;
                                break;
                            case 8:
                                finalValue = textBox3.Text;
                                break;

                        }

                        br.Close();

                        break;
                    case 1:
                        BinaryReader br1 = new BinaryReader(File.OpenRead(binAttributeFile));
                        var pr1 = "";

                        switch (stringAmount.Length)
                        {
                            case 2:

                                for (int i = 1; i <= 3; i++)
                                {
                                    br1.BaseStream.Position = pos + i;
                                    pr1 = br1.ReadByte().ToString("X");
                                    if (pr1.Length == 1)
                                    {
                                        pr1 = "0" + pr1;
                                        finalValue += pr1;
                                    }
                                    else
                                    {
                                        finalValue += pr1;
                                    }
                                }
                                finalValue = textBox3.Text + finalValue;
                                break;
                            case 4:

                                for (int i = 2; i <= 3; i++)
                                {
                                    br1.BaseStream.Position = pos + i;
                                    pr1 = br1.ReadByte().ToString("X");
                                    if (pr1.Length == 1)
                                    {
                                        pr1 = "0" + pr1;
                                        finalValue += pr1;
                                    }
                                    else
                                    {
                                        finalValue += pr1;
                                    }
                                }
                                finalValue = textBox3.Text + finalValue;
                                break;
                            case 6:
                                br1.BaseStream.Position = pos + 3;
                                pr1 = br1.ReadByte().ToString("X");
                                if (pr1.Length == 1)
                                {
                                    pr1 = "0" + pr1;
                                    finalValue += pr1;
                                }
                                else
                                {
                                    finalValue += pr1;
                                }
                                finalValue = textBox3.Text + finalValue;
                                break;
                            case 8:
                                finalValue = textBox3.Text;
                                break;

                        }

                        br1.Close();

                        break;
                    case 2:
                        BinaryReader br2 = new BinaryReader(File.OpenRead(tl2BinFile));
                        var pr2 = "";

                        switch (stringAmount.Length)
                        {
                            case 2:

                                for (int i = 1; i <= 3; i++)
                                {
                                    br2.BaseStream.Position = pos + i;
                                    pr2 = br2.ReadByte().ToString("X");
                                    if (pr2.Length == 1)
                                    {
                                        pr2 = "0" + pr2;
                                        finalValue += pr2;
                                    }
                                    else
                                    {
                                        finalValue += pr2;
                                    }
                                }
                                finalValue = textBox3.Text + finalValue;
                                break;
                            case 4:

                                for (int i = 2; i <= 3; i++)
                                {
                                    br2.BaseStream.Position = pos + i;
                                    pr2 = br2.ReadByte().ToString("X");
                                    if (pr2.Length == 1)
                                    {
                                        pr2 = "0" + pr2;
                                        finalValue += pr2;
                                    }
                                    else
                                    {
                                        finalValue += pr2;
                                    }
                                }
                                finalValue = textBox3.Text + finalValue;
                                break;
                            case 6:
                                br2.BaseStream.Position = pos + 3;
                                pr2 = br2.ReadByte().ToString("X");
                                if (pr2.Length == 1)
                                {
                                    pr2 = "0" + pr2;
                                    finalValue += pr2;
                                }
                                else
                                {
                                    finalValue += pr2;
                                }
                                finalValue = textBox3.Text + finalValue;
                                break;
                            case 8:
                                finalValue = textBox3.Text;
                                break;

                        }

                        br2.Close();

                        break;
                    case 3:
                        BinaryReader br3 = new BinaryReader(File.OpenRead(tl2BinAttributeFile));
                        var pr3 = "";

                        switch (stringAmount.Length)
                        {
                            case 2:

                                for (int i = 1; i <= 3; i++)
                                {
                                    br3.BaseStream.Position = pos + i;
                                    pr3 = br3.ReadByte().ToString("X");
                                    if (pr3.Length == 1)
                                    {
                                        pr3 = "0" + pr3;
                                        finalValue += pr3;
                                    }
                                    else
                                    {
                                        finalValue += pr3;
                                    }
                                }
                                finalValue = textBox3.Text + finalValue;
                                break;
                            case 4:

                                for (int i = 2; i <= 3; i++)
                                {
                                    br3.BaseStream.Position = pos + i;
                                    pr3 = br3.ReadByte().ToString("X");
                                    if (pr3.Length == 1)
                                    {
                                        pr3 = "0" + pr3;
                                        finalValue += pr3;
                                    }
                                    else
                                    {
                                        finalValue += pr3;
                                    }
                                }
                                finalValue = textBox3.Text + finalValue;
                                break;
                            case 6:
                                br3.BaseStream.Position = pos + 3;
                                pr3 = br3.ReadByte().ToString("X");
                                if (pr3.Length == 1)
                                {
                                    pr3 = "0" + pr3;
                                    finalValue += pr3;
                                }
                                else
                                {
                                    finalValue += pr3;
                                }
                                finalValue = textBox3.Text + finalValue;
                                break;
                            case 8:
                                finalValue = textBox3.Text;
                                break;

                        }

                        br3.Close();

                        break;
                }

                if (textBox3.Text != null && textBox4.Text != null)
                {
                    //BinaryWriter bw = new BinaryWriter(File.OpenWrite(binFile));

                    switch (comboBox4.SelectedIndex)
                    {
                        case 0:
                            BinaryWriter bw = new BinaryWriter(File.OpenWrite(binFile));
                            bw.BaseStream.Position = Int32.Parse(textBox4.Text, System.Globalization.NumberStyles.HexNumber);

                            int value = Int32.Parse(finalValue, System.Globalization.NumberStyles.HexNumber);
                            byte[] buffer = BitConverter.GetBytes(value);
                            Array.Reverse(buffer); //Permutar
                            bw.Write(buffer);

                            bw.Dispose();
                            break;
                        case 1:
                            BinaryWriter bw1 = new BinaryWriter(File.OpenWrite(binAttributeFile));
                            bw1.BaseStream.Position = Int32.Parse(textBox4.Text, System.Globalization.NumberStyles.HexNumber);

                            int value1 = Int32.Parse(finalValue, System.Globalization.NumberStyles.HexNumber);
                            byte[] buffer1 = BitConverter.GetBytes(value1);
                            Array.Reverse(buffer1); //Permutar
                            bw1.Write(buffer1);

                            bw1.Dispose();
                            break;
                        case 2:
                            BinaryWriter bw2 = new BinaryWriter(File.OpenWrite(tl2BinFile));
                            bw2.BaseStream.Position = Int32.Parse(textBox4.Text, System.Globalization.NumberStyles.HexNumber);

                            int value2 = Int32.Parse(finalValue, System.Globalization.NumberStyles.HexNumber);
                            byte[] buffer2 = BitConverter.GetBytes(value2);
                            Array.Reverse(buffer2); //Permutar
                            bw2.Write(buffer2);

                            bw2.Dispose();
                            break;
                        case 3:
                            BinaryWriter bw3 = new BinaryWriter(File.OpenWrite(tl2BinAttributeFile));
                            bw3.BaseStream.Position = Int32.Parse(textBox4.Text, System.Globalization.NumberStyles.HexNumber);

                            int value3 = Int32.Parse(finalValue, System.Globalization.NumberStyles.HexNumber);
                            byte[] buffer3 = BitConverter.GetBytes(value3);
                            Array.Reverse(buffer3); //Permutar
                            bw3.Write(buffer3);

                            bw3.Dispose();
                            break;
                    }
                }
                //reloadFile();
                //reloadFileAttribute();

                //OPEN
                
                int vValue = Int32.Parse(textBox4.Text, System.Globalization.NumberStyles.HexNumber) / 16;
                int hValue = Int32.Parse(textBox4.Text, System.Globalization.NumberStyles.HexNumber) % 16;

                int posProView = vValue * 59 + hValue * 3 + 10;
                //MessageBox.Show(finalValue.ToString());
                finalValue = finalValue.Substring(0, 2) + " " + finalValue.Substring(2, 2) + " " + finalValue.Substring(4, 2) + " " + finalValue.Substring(6, 2) + " ";
                //MessageBox.Show(finalValue.ToString());
                string valueProViewPre1 = "";
                string valueProViewPre2 = "";
                switch (comboBox4.SelectedIndex)
                {
                    case 0:
                        if (posProView % 59 > 47)
                        {
                            //MessageBox.Show((posProView % 59).ToString());
                            if (posProView % 59 == 49)
                            {
                                finalValue = finalValue.Substring(0, 9) + ultraString.Substring(posProView + 9, 11) + finalValue.Substring(9, 2);
                            } else if (posProView % 59 == 52)
                            {
                                finalValue = finalValue.Substring(0, 6) + ultraString.Substring(posProView + 6, 11) + finalValue.Substring(6, 2);
                            } else if (posProView % 59 == 55)
                            {
                                finalValue = finalValue.Substring(0, 3) + ultraString.Substring(posProView + 3, 11) + finalValue.Substring(3, 2);
                            }
                        }
                        valueProViewPre1 = ultraString.Substring(0, posProView);
                        valueProViewPre2 = ultraString.Substring(posProView + finalValue.Length);
                        ultraString = valueProViewPre1 + finalValue + valueProViewPre2;
                        richTextBox1.Text = ultraString;
                        break;
                    case 1:
                        if (posProView % 59 > 47)
                        {
                            //MessageBox.Show((posProView % 59).ToString());
                            if (posProView % 59 == 49)
                            {
                                finalValue = finalValue.Substring(0, 9) + ultraStringAttribute.Substring(posProView + 9, 11) + finalValue.Substring(9, 2);
                            }
                            else if (posProView % 59 == 52)
                            {
                                finalValue = finalValue.Substring(0, 6) + ultraStringAttribute.Substring(posProView + 6, 11) + finalValue.Substring(6, 2);
                            }
                            else if (posProView % 59 == 55)
                            {
                                finalValue = finalValue.Substring(0, 3) + ultraStringAttribute.Substring(posProView + 3, 11) + finalValue.Substring(3, 2);
                            }
                        }
                        valueProViewPre1 = ultraStringAttribute.Substring(0, posProView);
                        valueProViewPre2 = ultraStringAttribute.Substring(posProView + finalValue.Length);
                        ultraStringAttribute = valueProViewPre1 + finalValue + valueProViewPre2;
                        richTextBox2.Text = ultraStringAttribute;
                        break;
                    case 2:
                        if (posProView % 59 > 47)
                        {
                            //MessageBox.Show((posProView % 59).ToString());
                            if (posProView % 59 == 49)
                            {
                                finalValue = finalValue.Substring(0, 9) + tl2UltraString.Substring(posProView + 9, 11) + finalValue.Substring(9, 2);
                            }
                            else if (posProView % 59 == 52)
                            {
                                finalValue = finalValue.Substring(0, 6) + tl2UltraString.Substring(posProView + 6, 11) + finalValue.Substring(6, 2);
                            }
                            else if (posProView % 59 == 55)
                            {
                                finalValue = finalValue.Substring(0, 3) + tl2UltraString.Substring(posProView + 3, 11) + finalValue.Substring(3, 2);
                            }
                        }
                        valueProViewPre1 = tl2UltraString.Substring(0, posProView);
                        valueProViewPre2 = tl2UltraString.Substring(posProView + finalValue.Length);
                        tl2UltraString = valueProViewPre1 + finalValue + valueProViewPre2;
                        richTextBox3.Text = tl2UltraString;
                        break;
                    case 3:
                        if (posProView % 59 > 47)
                        {
                            //MessageBox.Show((posProView % 59).ToString());
                            if (posProView % 59 == 49)
                            {
                                finalValue = finalValue.Substring(0, 9) + tl2UltraStringAttribute.Substring(posProView + 9, 11) + finalValue.Substring(9, 2);
                            }
                            else if (posProView % 59 == 52)
                            {
                                finalValue = finalValue.Substring(0, 6) + tl2UltraStringAttribute.Substring(posProView + 6, 11) + finalValue.Substring(6, 2);
                            }
                            else if (posProView % 59 == 55)
                            {
                                finalValue = finalValue.Substring(0, 3) + tl2UltraStringAttribute.Substring(posProView + 3, 11) + finalValue.Substring(3, 2);
                            }
                        }
                        valueProViewPre1 = tl2UltraStringAttribute.Substring(0, posProView);
                        valueProViewPre2 = tl2UltraStringAttribute.Substring(posProView + finalValue.Length);
                        tl2UltraStringAttribute = valueProViewPre1 + finalValue + valueProViewPre2;
                        richTextBox4.Text = tl2UltraStringAttribute;
                        break;
                }

                /*
                if (usingTileset2 == true)
                {
                    string valueProViewPre1 = tl2UltraString.Substring(0, posProView);
                    string valueProViewPre2 = tl2UltraString.Substring(posProView + 6);
                    tl2UltraString = valueProViewPre1 + stringTile1 + " " + selectedPalette.ToString("X") + stringTile2 + " " + valueProViewPre2;
                    richTextBox3.Text = tl2UltraString;
                }
                else
                {
                    string valueProViewPre1 = ultraString.Substring(0, posProView);
                    string valueProViewPre2 = ultraString.Substring(posProView + 6);
                    ultraString = valueProViewPre1 + stringTile1 + " " + selectedPalette.ToString("X") + stringTile2 + " " + valueProViewPre2;
                    richTextBox1.Text = ultraString;
                }*/

                //CLOSE
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(574, this.Height);
            this.DoubleBuffered = true;
            normalToolStripMenuItem.Checked = true;
            folderManager();
        }


        private void setBlock_MouseClick(Object sender, MouseEventArgs e)
        {
            if (sender is PictureBox)
            {
                string nameBlock = ((PictureBox)sender).Name;
                int nBLength = nameBlock.Length;
                nameBlock = nameBlock.Substring(5);
                blockPicked = int.Parse(nameBlock);
                string totalBlocks = int.Parse(nameBlock).ToString("X");
                switch (totalBlocks.Length)
                {
                    case 1:
                        totalBlocks = "00" + totalBlocks;
                        break;
                    case 2:
                        totalBlocks = "0" + totalBlocks;
                        break;
                }
                label1.Text = "Tile: " + totalBlocks;
                resetFlipSystem();
                createNewBlock(int.Parse(nameBlock));
                
            }
        }

        private void setBlock_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is PictureBox)
            {
                string nameBlock = ((PictureBox)sender).Name;
                int nBLength = nameBlock.Length;
                nameBlock = nameBlock.Substring(5);
                string totalBlocks = int.Parse(nameBlock).ToString("X");
                switch (totalBlocks.Length)
                {
                    case 1:
                        totalBlocks = "00" + totalBlocks;
                        break;
                    case 2:
                        totalBlocks = "0" + totalBlocks;
                        break;
                }
                label1.Text = "Tile: " + totalBlocks;

            }
        }

        private void setBlock_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                string totalBlocks = (blockPicked).ToString("X");
                switch (totalBlocks.Length)
                {
                    case 1:
                        totalBlocks = "00" + totalBlocks;
                        break;
                    case 2:
                        totalBlocks = "0" + totalBlocks;
                        break;
                }
                label1.Text = "Tile: " + totalBlocks;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void createNewBlock(int positionInDec)
        {
            PictureBox picBlock = this.Controls.OfType<PictureBox>().FirstOrDefault(a => a.Name == "blockSelected");
            if (picBlock != null)
            {
                this.Controls.Remove(picBlock);
            }

            var finalPosition = 0;
            bool usingTileset2 = false;

            if (positionInDec > 511)
            {
                finalPosition = positionInDec - 512;
                usingTileset2 = true;
            } else
            {
                finalPosition = positionInDec;
                usingTileset2 = false;
            }

            

            var x = finalPosition % 16;
            var y = finalPosition / 16;
            //MessageBox.Show(finalPosition.ToString());
            if (tileset != null)
            {
                var img = tileset;

                if (usingTileset2 == true)
                {
                    img = tl2Tileset;
                } else
                {
                    img = tileset;
                }
                var setBlock = new Bitmap(8, 8);
                var graphics = Graphics.FromImage(setBlock);
                graphics.DrawImage(img, new Rectangle(0, 0, 8, 8), new Rectangle(x * 8, y * 8, 8, 8), GraphicsUnit.Pixel);
                graphics.Dispose();

                var picture = new PictureBox
                {
                    Name = "blockSelected",
                    Size = new Size(24, 24),
                    Location = new Point(296, 106),
                    Image = setBlock,

                };
                this.Controls.Add(picture);
                PictureBox picBlock2 = this.Controls.OfType<PictureBox>().FirstOrDefault(a => a.Name == "blockSelected");
                if (picBlock2 != null)
                {
                    int wid = (int)(picBlock2.Image.Width * 3);
                    int hgt = (int)(picBlock2.Image.Height * 3);
                    Bitmap bm = new Bitmap(wid, hgt);
                    using (Graphics gr = Graphics.FromImage(bm))
                    {
                        // No smoothing.
                        gr.InterpolationMode = InterpolationMode.NearestNeighbor;

                        Point[] dest =
                        {
                        new Point(0, 0),
                        new Point(wid, 0),
                        new Point(0, hgt),
                    };
                        Rectangle source = new Rectangle(
                            0, 0,
                            picBlock2.Image.Width,
                            picBlock2.Image.Height);
                        gr.DrawImage(picBlock2.Image,
                            dest, source, GraphicsUnit.Pixel);
                    }

                    picBlock2.Image = bm;
                }

            }
        }

        private void setColorTransparency(int paletteNumber, int colorNumber, bool isTransparent) {
            var palReference = palettes[paletteNumber].Entries[colorNumber];
            if (isTransparent == true)
            {
                palettes[paletteNumber].Entries[colorNumber] = Color.FromArgb(0, palReference.R, palReference.G, palReference.B);
            }
            else
            {
                palettes[paletteNumber].Entries[colorNumber] = Color.FromArgb(255, palReference.R, palReference.G, palReference.B);
            }

        }

        private string checkPaletteXY(string pal) {
            string a = pal.Substring(0, 1);
            return a;
        }
        private string checkFirstChar(string number)
        {
            switch (number)
            {
                case "0":
                    xFlipped = false;
                    yFlipped = false;
                    return "0";
                case "1":
                    xFlipped = false;
                    yFlipped = false;
                    return "1";
                case "2":
                    xFlipped = false;
                    yFlipped = false;
                    return "2";
                case "3":
                    xFlipped = false;
                    yFlipped = false;
                    return "3";
                case "4":
                    xFlipped = true;
                    yFlipped = false;
                    return "0";
                case "5":
                    xFlipped = true;
                    yFlipped = false;
                    return "1";
                case "6":
                    xFlipped = true;
                    yFlipped = false;
                    return "2";
                case "7":
                    xFlipped = true;
                    yFlipped = false;
                    return "3";
                case "8":
                    xFlipped = false;
                    yFlipped = true;
                    return "0";
                case "9":
                    xFlipped = false;
                    yFlipped = true;
                    return "1";
                case "A":
                    xFlipped = false;
                    yFlipped = true;
                    return "2";
                case "B":
                    xFlipped = false;
                    yFlipped = true;
                    return "3";
                case "C":
                    xFlipped = true;
                    yFlipped = true;
                    return "0";
                case "D":
                    xFlipped = true;
                    yFlipped = true;
                    return "1";
                case "E":
                    xFlipped = true;
                    yFlipped = true;
                    return "2";
                case "F":
                    xFlipped = true;
                    yFlipped = true;
                    return "3";
                default:
                    xFlipped = false;
                    yFlipped = false;
                    return "0";
            }
        }

        private void loadBlockTiles()
        {
            string workingString = completeValue;
            var constructingBlock = 0;
            var totalLines = (bytesToLoad + tl2BytesToLoad) / 16;
            var xLineBlock = 1;

            for (int i = 0; i < tileBlockCounter; i++) { 
                PictureBox picBlock = panel2.Controls.OfType<PictureBox>().FirstOrDefault(x => x.Name == "blockTile" + i.ToString());
                if (picBlock != null)
                {
                    panel2.Controls.Remove(picBlock);
                    //panel2.Controls.Clear();
                }
            }
            
            tileBlockCounter = 0;

            for (int i = 0; i < totalLines; i++)
            { //Recorre la línea completa
                var a = workingString.Substring(0, 32); //Fija la primera mitad de la línea a "a"
                var img = tileset;
                var setBlock = new Bitmap(16, 16);
                Graphics grph = Graphics.FromImage(setBlock);

                for (int k = 0; k < 8; k++) {
                    var tile = int.Parse(checkFirstChar(a.Substring(k * 4 + 3, 1)) + a.Substring(k * 4, 2), System.Globalization.NumberStyles.HexNumber);
                    var pal = int.Parse(a.Substring(k * 4 + 2, 1));
                    if (tile > bytesToLoad / 16)
                    {
                        tile = tile - (bytesToLoad / 16);
                        //MessageBox.Show((bytesToLoad / 16).ToString());
                        img = tl2Tileset;
                    } else
                    {
                        img = tileset;
                    }
                    int x = (tile % 16) * 8;
                    int y = (tile / 16) * 8;
                    if (k < 4) {
                        setColorTransparency(pal, 0, false);
                    } else
                    {
                        setColorTransparency(pal, 0, true);
                    }
                    setPalette(pal);
                    int xSum = 0;
                    int ySum = 0;

                    switch (k)
                    {
                        case 0:
                            xSum = 0;
                            ySum = 0;
                            break;
                        case 1:
                            xSum = 8;
                            ySum = 0;
                            break;
                        case 2:
                            xSum = 0;
                            ySum = 8;
                            break;
                        case 3:
                            xSum = 8;
                            ySum = 8;
                            break;
                        case 4:
                            xSum = 0;
                            ySum = 0;
                            break;
                        case 5:
                            xSum = 8;
                            ySum = 0;
                            break;
                        case 6:
                            xSum = 0;
                            ySum = 8;
                            break;
                        case 7:
                            xSum = 8;
                            ySum = 8;
                            break;
                    }

                    if (xFlipped == false && yFlipped == false)
                    {
                        grph.DrawImage(img, new Rectangle(xSum, ySum, 8, 8), new Rectangle(x, y, 8, 8), GraphicsUnit.Pixel);
                    }
                    else
                    {
                        var checkFlipBlock = new Bitmap(8, 8);
                        Graphics grphFlip = Graphics.FromImage(checkFlipBlock);
                        grphFlip.DrawImage(img, new Rectangle(0, 0, 8, 8), new Rectangle(x, y, 8, 8), GraphicsUnit.Pixel);
                        Bitmap toFlipImg = checkFlipBlock;

                        if (xFlipped == true)
                        {
                            if (yFlipped == true)
                            {
                                toFlipImg.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                                grph.DrawImage(toFlipImg, new Rectangle(xSum, ySum, 8, 8), new Rectangle(0, 0, 8, 8), GraphicsUnit.Pixel);
                            }
                            else
                            {
                                toFlipImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                grph.DrawImage(toFlipImg, new Rectangle(xSum, ySum, 8, 8), new Rectangle(0, 0, 8, 8), GraphicsUnit.Pixel);
                            }
                        }
                        else if (yFlipped == true)
                        {
                            toFlipImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            grph.DrawImage(toFlipImg, new Rectangle(xSum, ySum, 8, 8), new Rectangle(0, 0, 8, 8), GraphicsUnit.Pixel);
                        }
                        grphFlip.Dispose();
                    }
                    //grph.DrawImage(img, new Rectangle(xSum, ySum, 8, 8), new Rectangle(x, y, 8, 8), GraphicsUnit.Pixel);
                }
                grph.Dispose();

                var picture = new PictureBox
                {
                    Name = "blockTile" + i.ToString(),
                    Size = new Size(16, 16),
                    Location = new Point(((constructingBlock % 8) * 16), ((constructingBlock / 8) * 16)),
                    Image = setBlock,

                };

                picture.Image = setBlock;
                picture.MouseClick += setTileBlock_MouseClick;
                picture.Cursor = Cursors.Hand;
                panel2.Controls.Add(picture);

                if (xLineBlock < 9) { xLineBlock++; } else { xLineBlock = 1; }
                workingString = workingString.Substring(32); //Elimina los primeros 16 bytes de la línea para seguir trabajando con ella
                constructingBlock++; //Suma un bloque creado
                tileBlockCounter++;
            }
            for (int i = 0; i < palettesLoaded; i++)
            {
                setColorTransparency(i, 0, false);
            }
        }

        private void setTileBlock_MouseClick(Object sender, MouseEventArgs e)
        {
            if (sender is PictureBox) {
                var initialPalette = selectedPalette;
                var txtDef = ((PictureBox)sender).Name.ToString();
                var bloque = txtDef.Substring("blockTile".Length, (txtDef.Length - "blockTile".Length));
                var stringBlock = completeValue.Substring(Int32.Parse(bloque)*32, 32);
                var img = tileset;

                var strtXFlipped = xFlipped;
                var strtYFlipped = yFlipped;

                var attributeData = "";
                //MessageBox.Show("Click en el bloque: " + stringBlock);

                label7.Text = "Block: " + bloque.ToString();

                blockSelected = bloque;

                attributeData = completeValueAttribute.Substring(Int32.Parse(blockSelected) * 4, 4);
                comboBox2.SelectedIndex = int.Parse(attributeData.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                comboBox3.SelectedIndex = int.Parse(attributeData.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                //MessageBox.Show(attributeData);

                for (int i = 0; i < 8; i++)
                {
                    var tile = int.Parse(checkFirstChar(stringBlock.Substring(i * 4 + 3, 1)) + stringBlock.Substring(i * 4, 2), System.Globalization.NumberStyles.HexNumber);
                    var pal = int.Parse(stringBlock.Substring(i * 4 + 2, 1));
                    if (tile > bytesToLoad / 16)
                    {
                        tile = tile - (bytesToLoad / 16);
                        //MessageBox.Show((bytesToLoad / 16).ToString());
                        img = tl2Tileset;
                        //MessageBox.Show("Executed: " + tile.ToString());
                    }
                    else
                    {
                        img = tileset;
                    }
                    int xn = (tile % 16) * 8;
                    int yn = (tile / 16) * 8;
                    var setBlock = new Bitmap(24, 24);
                    Graphics grph = Graphics.FromImage(setBlock);
                    setPalette(pal);

                    var x = 0;
                    var y = 0;
                    switch (i) {
                        case 0:
                            x = 15;
                            y = 80;
                            break;
                        case 1:
                            x = 39;
                            y = 80;
                            break;
                        case 2:
                            x = 15;
                            y = 104;
                            break;
                        case 3:
                            x = 39;
                            y = 104;
                            break;
                        case 4:
                            x = 63;
                            y = 80;
                            break;
                        case 5:
                            x = 87;
                            y = 80;
                            break;
                        case 6:
                            x = 63;
                            y = 104;
                            break;
                        case 7:
                            x = 87;
                            y = 104;
                            break;
                    }
                    PictureBox picBlock = this.Controls.OfType<PictureBox>().FirstOrDefault(a => a.Name == "blockTileHigh" + i.ToString());
                    if (picBlock != null)
                    {
                        this.Controls.Remove(picBlock);
                    }
                    if (xFlipped == false && yFlipped == false)
                    {
                        grph.DrawImage(img, new Rectangle(0, 0, 8, 8), new Rectangle(xn, yn, 8, 8), GraphicsUnit.Pixel);
                    }
                    else
                    {
                        var checkFlipBlock = new Bitmap(8, 8);
                        Graphics grphFlip = Graphics.FromImage(checkFlipBlock);
                        grphFlip.DrawImage(img, new Rectangle(0, 0, 8, 8), new Rectangle(xn, yn, 8, 8), GraphicsUnit.Pixel);
                        Bitmap toFlipImg = checkFlipBlock;

                        if (xFlipped == true)
                        {
                            if (yFlipped == true)
                            {
                                toFlipImg.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                                grph.DrawImage(toFlipImg, new Rectangle(0, 0, 8, 8), new Rectangle(0, 0, 8, 8), GraphicsUnit.Pixel);
                            }
                            else
                            {
                                toFlipImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                grph.DrawImage(toFlipImg, new Rectangle(0, 0, 8, 8), new Rectangle(0, 0, 8, 8), GraphicsUnit.Pixel);
                            }
                        }
                        else if (yFlipped == true)
                        {
                            toFlipImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            grph.DrawImage(toFlipImg, new Rectangle(0, 0, 8, 8), new Rectangle(0, 0, 8, 8), GraphicsUnit.Pixel);
                        }
                        grphFlip.Dispose();
                    }
                    grph.Dispose();


                    var picture = new PictureBox
                    {
                        Name = "blockTileHigh" + i.ToString(),
                        Size = new Size(24, 24),
                        Location = new Point(x, y),
                        Image = setBlock,

                    };
                    picture.Image = setBlock;
                    picture.Cursor = Cursors.Hand;
                    //picture.MouseClick += editTileBlock_MouseClick;
                    picture.MouseClick += (sender2, e2) => editTileBlock_MouseClick(sender2, e2, bloque);
                    this.Controls.Add(picture);

                    PictureBox picBlock2 = this.Controls.OfType<PictureBox>().FirstOrDefault(a => a.Name == "blockTileHigh" + i.ToString());
                    if (picBlock2 != null)
                    {
                        int wid = (int)(picBlock2.Image.Width * 3);
                        int hgt = (int)(picBlock2.Image.Height * 3);
                        Bitmap bm = new Bitmap(wid, hgt);
                        using (Graphics gr = Graphics.FromImage(bm))
                        {
                            // No smoothing.
                            gr.InterpolationMode = InterpolationMode.NearestNeighbor;

                            Point[] dest =
                            {
                        new Point(0, 0),
                        new Point(wid, 0),
                        new Point(0, hgt),
                    };
                            Rectangle source = new Rectangle(
                                0, 0,
                                picBlock2.Image.Width,
                                picBlock2.Image.Height);
                            gr.DrawImage(picBlock2.Image,
                                dest, source, GraphicsUnit.Pixel);
                        }

                        picBlock2.Image = bm;
                    }
                }
                selectedPalette = initialPalette;
                setPalette(selectedPalette);
                xFlipped = strtXFlipped;
                yFlipped = strtYFlipped;
            }
        }

        private void editTileBlock_MouseClick(Object sender, MouseEventArgs e, string block)
        {
            if (sender is PictureBox)
            {
                var txtDef = (((PictureBox)sender).Name.ToString()).Substring("blockTileHigh".Length);
                var bloque = block;
                var bloqueNumeroString = bloque;
                var stringBlock = completeValue.Substring(Int32.Parse(bloque) * 32, 32);
                var stringTile1 = (blockPicked).ToString("X");
                var stringTile2 = "0";
                var stringByte = "";
                var starterPalette = selectedPalette;

                var strtXFlipped = xFlipped;
                var strtYFlipped = yFlipped;

                if (stringTile1.Length > 2)
                {
                    stringTile2 = stringTile1.Substring(0, 1).ToString();
                }

                if (xFlipped == true)
                {
                    stringTile2 = (Int32.Parse(stringTile2) + 4).ToString();
                }
                if (yFlipped == true)
                {
                    stringTile2 = (Int32.Parse(stringTile2) + 8).ToString();
                }

                switch (stringTile2)
                {
                    case "0":
                        xFlipped = false;
                        yFlipped = false;
                        stringTile2 = "0";
                        break;
                    case "1":
                        xFlipped = false;
                        yFlipped = false;
                        stringTile2 = "1";
                        break;
                    case "2":
                        xFlipped = false;
                        yFlipped = false;
                        stringTile2 = "2";
                        break;
                    case "3":
                        xFlipped = false;
                        yFlipped = false;
                        stringTile2 = "3";
                        break;
                    case "4":
                        xFlipped = true;
                        yFlipped = false;
                        stringTile2 = "4";
                        break;
                    case "5":
                        xFlipped = true;
                        yFlipped = false;
                        stringTile2 = "5";
                        break;
                    case "6":
                        xFlipped = true;
                        yFlipped = false;
                        stringTile2 = "6";
                        break;
                    case "7":
                        xFlipped = true;
                        yFlipped = false;
                        stringTile2 = "7";
                        break;
                    case "8":
                        xFlipped = false;
                        yFlipped = true;
                        stringTile2 = "8";
                        break;
                    case "9":
                        xFlipped = false;
                        yFlipped = true;
                        stringTile2 = "9";
                        break;
                    case "10":
                        xFlipped = false;
                        yFlipped = true;
                        stringTile2 = "A";
                        break;
                    case "11":
                        xFlipped = false;
                        yFlipped = true;
                        stringTile2 = "B";
                        break;
                    case "12":
                        xFlipped = true;
                        yFlipped = true;
                        stringTile2 = "C";
                        break;
                    case "13":
                        xFlipped = true;
                        yFlipped = true;
                        stringTile2 = "D";
                        break;
                    case "14":
                        xFlipped = true;
                        yFlipped = true;
                        stringTile2 = "E";
                        break;
                    case "15":
                        xFlipped = true;
                        yFlipped = true;
                        stringTile2 = "F";
                        break;
                }

                if (stringTile1.Length == 1)
                {
                    stringTile1 = "0" + stringTile1;
                } else if (stringTile1.Length == 3)
                {
                    stringTile1 = stringTile1.Substring(1, 2);
                }
                stringByte = stringTile1 + selectedPalette.ToString("X") + stringTile2;
                //MessageBox.Show(stringByte);
                //MessageBox.Show((Int32.Parse(bloque) * 16 + Int32.Parse(txtDef) * 2).ToString());

                BinaryReader br = new BinaryReader(File.OpenRead(binFile));
                var firstFileLength = Int32.Parse((br.BaseStream.Length / 16).ToString());
                br.Close();
                var usingTileset2 = false;
                //MessageBox.Show(bloque.ToString());
                if (Int32.Parse(bloque) >= firstFileLength)
                {
                    bloque = (Int32.Parse(bloque) - firstFileLength).ToString();
                    //insertBytes((Int32.Parse(blockSelected) * 2).ToString("X"), byteToLoad, tl2BinAttributeFile);
                    insertBytes((Int32.Parse(bloque) * 16 + Int32.Parse(txtDef) * 2).ToString("X"), stringByte, tl2BinFile);
                    usingTileset2 = true;
                }
                else
                {
                    insertBytes((Int32.Parse(bloque) * 16 + Int32.Parse(txtDef) * 2).ToString("X"), stringByte, binFile);
                    usingTileset2 = false;
                }

                //insertBytes((Int32.Parse(bloque) * 16 + Int32.Parse(txtDef) * 2).ToString("X"), stringByte, binFile); //Edit file
               // bloque = bloqueNumeroString;

                int posProView = Int32.Parse(bloque) * 59 + Int32.Parse(txtDef) * 6 + 10;
                int posString = Int32.Parse(bloque) * 32 + Int32.Parse(txtDef) * 4;

                if (usingTileset2 == true)
                {
                    string valueProViewPre1 = tl2UltraString.Substring(0, posProView);
                    string valueProViewPre2 = tl2UltraString.Substring(posProView + 6);
                    tl2UltraString = valueProViewPre1 + stringTile1 + " " + selectedPalette.ToString("X") + stringTile2 + " " + valueProViewPre2;
                    richTextBox3.Text = tl2UltraString;
                } else { 
                    string valueProViewPre1 = ultraString.Substring(0, posProView);
                    string valueProViewPre2 = ultraString.Substring(posProView + 6);
                    ultraString = valueProViewPre1 + stringTile1 + " " + selectedPalette.ToString("X") + stringTile2 + " " + valueProViewPre2;
                    richTextBox1.Text = ultraString;
                }
                bloque = bloqueNumeroString;
                posProView = Int32.Parse(bloque) * 59 + Int32.Parse(txtDef) * 6 + 10;
                posString = Int32.Parse(bloque) * 32 + Int32.Parse(txtDef) * 4;

                string valueStringPre1 = completeValue.Substring(0, posString);
                string valueStringPre2 = completeValue.Substring(posString + 4);
                completeValue = valueStringPre1 + stringTile1 + selectedPalette.ToString("X") + stringTile2 + valueStringPre2;

                //Reload pictures
                var tileDec = int.Parse((checkFirstChar(stringTile2) + stringTile1), System.Globalization.NumberStyles.HexNumber);
                var img = tileset;
                if (tileDec > bytesToLoad / 16)
                {
                    tileDec = tileDec - (bytesToLoad / 16);
                    //MessageBox.Show((bytesToLoad / 16).ToString());
                    img = tl2Tileset;
                    //MessageBox.Show("Executed: " + tile.ToString());
                }
                else
                {
                    img = tileset;
                }
                var setBlock = new Bitmap(24, 24);
                Graphics grph = Graphics.FromImage(setBlock);
                setPalette(selectedPalette);

                var x = 0;
                var y = 0;
                int xn = (tileDec % 16) * 8;
                int yn = (tileDec / 16) * 8;
                switch (Int32.Parse((((PictureBox)sender).Name.ToString()).Substring("blockTileHigh".Length)))
                {
                    case 0:
                        x = 15;
                        y = 80;
                        break;
                    case 1:
                        x = 39;
                        y = 80;
                        break;
                    case 2:
                        x = 15;
                        y = 104;
                        break;
                    case 3:
                        x = 39;
                        y = 104;
                        break;
                    case 4:
                        x = 63;
                        y = 80;
                        break;
                    case 5:
                        x = 87;
                        y = 80;
                        break;
                    case 6:
                        x = 63;
                        y = 104;
                        break;
                    case 7:
                        x = 87;
                        y = 104;
                        break;
                }
                PictureBox picBlock2 = this.Controls.OfType<PictureBox>().FirstOrDefault(a => a.Name == "blockTileHigh" + ((((PictureBox)sender).Name.ToString()).Substring("blockTileHigh".Length)));
                if (xFlipped == false && yFlipped == false)
                {
                    grph.DrawImage(img, new Rectangle(0, 0, 8, 8), new Rectangle(xn, yn, 8, 8), GraphicsUnit.Pixel);
                }
                else
                {
                    var checkFlipBlock = new Bitmap(8, 8);
                    Graphics grphFlip = Graphics.FromImage(checkFlipBlock);
                    grphFlip.DrawImage(img, new Rectangle(0, 0, 8, 8), new Rectangle(xn, yn, 8, 8), GraphicsUnit.Pixel);
                    Bitmap toFlipImg = checkFlipBlock;

                    if (xFlipped == true)
                    {
                        if (yFlipped == true)
                        {
                            toFlipImg.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                            grph.DrawImage(toFlipImg, new Rectangle(0, 0, 8, 8), new Rectangle(0, 0, 8, 8), GraphicsUnit.Pixel);
                        }
                        else
                        {
                            toFlipImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            grph.DrawImage(toFlipImg, new Rectangle(0, 0, 8, 8), new Rectangle(0, 0, 8, 8), GraphicsUnit.Pixel);
                        }
                    }
                    else if (yFlipped == true)
                    {
                        toFlipImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        grph.DrawImage(toFlipImg, new Rectangle(0, 0, 8, 8), new Rectangle(0, 0, 8, 8), GraphicsUnit.Pixel);
                    }
                    grphFlip.Dispose();
                }
                grph.Dispose();

                PictureBox picBlock = this.Controls.OfType<PictureBox>().FirstOrDefault(a => a.Name == "blockTileHigh" + ((((PictureBox)sender).Name.ToString()).Substring("blockTileHigh".Length)));
                if (picBlock != null)
                {
                    picBlock.Image = setBlock;
                    picBlock.Location = new Point(x, y);
                }
                else
                {
                    MessageBox.Show("No existe " + "blockTileHigh" + ((((PictureBox)sender).Name.ToString()).Substring("blockTileHigh".Length)));
                }

                PictureBox picBlock3 = this.Controls.OfType<PictureBox>().FirstOrDefault(a => a.Name == "blockTileHigh" + ((((PictureBox)sender).Name.ToString()).Substring("blockTileHigh".Length)));
                if (picBlock3 != null)
                {
                    int wid = (int)(picBlock3.Image.Width * 3);
                    int hgt = (int)(picBlock3.Image.Height * 3);
                    Bitmap bm = new Bitmap(wid, hgt);
                    using (Graphics gr = Graphics.FromImage(bm))
                    {
                        // No smoothing.
                        gr.InterpolationMode = InterpolationMode.NearestNeighbor;

                        Point[] dest =
                        {
                        new Point(0, 0),
                        new Point(wid, 0),
                        new Point(0, hgt),
                    };
                        Rectangle source = new Rectangle(
                            0, 0,
                            picBlock3.Image.Width,
                            picBlock3.Image.Height);
                        gr.DrawImage(picBlock3.Image,
                            dest, source, GraphicsUnit.Pixel);
                    }

                    picBlock3.Image = bm;
                }
                
                var b = completeValue.Substring(Int32.Parse(bloque) * 32, 32); //Fija la primera mitad de la línea a "a"
                var img2 = tileset;
                var setBlock2 = new Bitmap(16, 16);
                Graphics grph2 = Graphics.FromImage(setBlock2);

                for (int k = 0; k < 8; k++)
                {
                    var tile = int.Parse(checkFirstChar(b.Substring(k * 4 + 3, 1)) + b.Substring(k * 4, 2), System.Globalization.NumberStyles.HexNumber);
                    var pal = int.Parse(b.Substring(k * 4 + 2, 1));
                    //MessageBox.Show("tile: " + tile.ToString());
                    if (tile > bytesToLoad / 16)
                    {
                        tile = tile - (bytesToLoad / 16);
                        //MessageBox.Show((bytesToLoad / 16).ToString());
                        img2 = tl2Tileset;
                        //MessageBox.Show("Executed: " + tile.ToString());
                        //MessageBox.Show("Executed");
                    }
                    else
                    {
                        img2 = tileset;
                    }
                    //MessageBox.Show("tile: " + tile.ToString());
                    int x2 = (tile % 16) * 8;
                    int y2 = (tile / 16) * 8;
                    if (k < 4)
                    {
                        setColorTransparency(pal, 0, false);
                    }
                    else
                    {
                        setColorTransparency(pal, 0, true);
                    }
                    setPalette(pal);
                    int xSum = 0;
                    int ySum = 0;

                    switch (k)
                    {
                        case 0:
                            xSum = 0;
                            ySum = 0;
                            break;
                        case 1:
                            xSum = 8;
                            ySum = 0;
                            break;
                        case 2:
                            xSum = 0;
                            ySum = 8;
                            break;
                        case 3:
                            xSum = 8;
                            ySum = 8;
                            break;
                        case 4:
                            xSum = 0;
                            ySum = 0;
                            break;
                        case 5:
                            xSum = 8;
                            ySum = 0;
                            break;
                        case 6:
                            xSum = 0;
                            ySum = 8;
                            break;
                        case 7:
                            xSum = 8;
                            ySum = 8;
                            break;
                    }

                    if (xFlipped == false && yFlipped == false)
                    {
                        grph2.DrawImage(img2, new Rectangle(xSum, ySum, 8, 8), new Rectangle(x2, y2, 8, 8), GraphicsUnit.Pixel);
                    }
                    else
                    {
                        var checkFlipBlock = new Bitmap(8, 8);
                        Graphics grphFlip = Graphics.FromImage(checkFlipBlock);
                        grphFlip.DrawImage(img2, new Rectangle(0, 0, 8, 8), new Rectangle(x2, y2, 8, 8), GraphicsUnit.Pixel);
                        Bitmap toFlipImg = checkFlipBlock;

                        if (xFlipped == true)
                        {
                            if (yFlipped == true)
                            {
                                toFlipImg.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                                grph2.DrawImage(toFlipImg, new Rectangle(xSum, ySum, 8, 8), new Rectangle(0, 0, 8, 8), GraphicsUnit.Pixel);
                            }
                            else
                            {
                                toFlipImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                grph2.DrawImage(toFlipImg, new Rectangle(xSum, ySum, 8, 8), new Rectangle(0, 0, 8, 8), GraphicsUnit.Pixel);
                            }
                        }
                        else if (yFlipped == true)
                        {
                            toFlipImg.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            grph2.DrawImage(toFlipImg, new Rectangle(xSum, ySum, 8, 8), new Rectangle(0, 0, 8, 8), GraphicsUnit.Pixel);
                        }
                        grphFlip.Dispose();
                    }
                    //grph.DrawImage(img, new Rectangle(xSum, ySum, 8, 8), new Rectangle(x, y, 8, 8), GraphicsUnit.Pixel);
                }
                grph.Dispose();

                PictureBox picBlock4 = panel2.Controls.OfType<PictureBox>().FirstOrDefault(a => a.Name == "blockTile" + bloqueNumeroString.ToString());
                if (picBlock4 != null)
                {
                    picBlock4.Image = setBlock2;
                }
                for (int i = 0; i < palettesLoaded; i++)
                {
                    setColorTransparency(i, 0, false);
                }
                selectedPalette = starterPalette;
                setPalette(selectedPalette);
                xFlipped = strtXFlipped;
                yFlipped = strtYFlipped;


            }
        }

        private void insertBytes(string position, string BytesToAdd, string fileToEdit) {
            int pos = int.Parse(position, System.Globalization.NumberStyles.HexNumber);
            var stringByte = BytesToAdd;
            string finalValue = null;

            BinaryReader br = new BinaryReader(File.OpenRead(fileToEdit));

            var pr = "";
            //MessageBox.Show(pos.ToString("X").Substring(pos.ToString("X").Length - 1).ToString());
            if ((pos.ToString("X").Substring(pos.ToString("X").Length - 1) == "C" || pos.ToString("X").Substring(pos.ToString("X").Length - 1) == "D") || (pos.ToString("X").Substring(pos.ToString("X").Length - 1) == "E" || pos.ToString("X").Substring(pos.ToString("X").Length - 1) == "F"))
            {
                for (int i = -2; i <= -1; i++)
                {
                    br.BaseStream.Position = pos + i;
                    pr = br.ReadByte().ToString("X");
                    if (pr.Length == 1)
                    {
                        pr = "0" + pr;
                        finalValue += pr;
                    }
                    else
                    {
                        finalValue += pr;
                    }
                }
                finalValue = finalValue + stringByte;
            } else
            {
                for (int i = 2; i <= 3; i++)
                {
                    br.BaseStream.Position = pos + i;
                    pr = br.ReadByte().ToString("X");
                    if (pr.Length == 1)
                    {
                        pr = "0" + pr;
                        finalValue += pr;
                    }
                    else
                    {
                        finalValue += pr;
                    }
                }
                finalValue = stringByte + finalValue;
            }
            

            br.Close();

            if (stringByte != null && position != null)
            {
                BinaryWriter bw = new BinaryWriter(File.OpenWrite(fileToEdit));

                if ((pos.ToString("X").Substring(pos.ToString("X").Length - 1) == "C" || pos.ToString("X").Substring(pos.ToString("X").Length - 1) == "D") || (pos.ToString("X").Substring(pos.ToString("X").Length - 1) == "E" || pos.ToString("X").Substring(pos.ToString("X").Length - 1) == "F"))
                {
                    bw.BaseStream.Position = Int32.Parse(position, System.Globalization.NumberStyles.HexNumber) - 2;
                } else
                {
                    bw.BaseStream.Position = Int32.Parse(position, System.Globalization.NumberStyles.HexNumber);
                }

                //bw.BaseStream.Position = Int32.Parse(position, System.Globalization.NumberStyles.HexNumber);

                int value = Int32.Parse(finalValue, System.Globalization.NumberStyles.HexNumber);
                byte[] buffer = BitConverter.GetBytes(value);
                Array.Reverse(buffer); //Permutar
                bw.Write(buffer);

                bw.Dispose();
            }

            //reloadFile();
        }

        private int searchTileByNum(int tileNum, bool returnX){
            int positionInDec = int.Parse(tileNum.ToString(), System.Globalization.NumberStyles.HexNumber);
            int xPos = (positionInDec % 16) * 8;
            int yPos = (positionInDec / 16) * 8;
            if (returnX == true)
            {
                return xPos;
            } else
            {
                return yPos;
            }
            //MessageBox.Show("xPosByPixels: " + xPos + Environment.NewLine + "yPosByPixels: " + yPos);
        }
        /*
        private void setTileBlocks(int pos, int pal, bool upperBlock) {
            var imgarray = new Image[1];
            if (tileset != null)
            {
                //MessageBox.Show("Executed");
                var img = tileset;
                imgarray[0] = new Bitmap(8, 8);
                var graphics = Graphics.FromImage(imgarray[0]);
                graphics.DrawImage(img, new Rectangle(0, 0, 8, 8), new Rectangle(8, 8, 8, 8), GraphicsUnit.Pixel);
                graphics.Dispose();

                var picture = new PictureBox
                {
                    Name = "blockTile" + 0.ToString(),
                    Size = new Size(8, 8),
                    Location = new Point(0, 0),
                    Image = imgarray[0],
                    Visible = true,

                };
                picture.MouseClick += setBlock_MouseClick;
                picture.MouseMove += setBlock_MouseMove;
                picture.MouseLeave += setBlock_MouseLeave;
                panel1.Controls.Add(picture);
                blockCounter++;
            }
        }*/

        private void createBlocks()
        {
            

            int hgt = tileset.Height;
            int filas = hgt / 8;
            int totalBlocks = filas * 16;

            if (blockCounter > 0) {
                PictureBox picBlock = this.Controls.OfType<PictureBox>().FirstOrDefault(x => x.Name == "blockSelected");
                if (picBlock != null)
                {
                    this.Controls.Remove(picBlock);
                }
                panel1.Visible = false;
                panel1.Controls.Clear();
            }

            blockCounter = 0;

            var imgarray = new Image[totalBlocks];
            if (tileset != null)
            {
                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        var img = tileset;
                        imgarray[blockCounter] = new Bitmap(8, 8);
                        var graphics = Graphics.FromImage(imgarray[blockCounter]);
                        graphics.DrawImage(img, new Rectangle(0, 0, 8, 8), new Rectangle(j * 8, i * 8, 8, 8), GraphicsUnit.Pixel);
                        graphics.Dispose();

                        var picture = new PictureBox
                        {
                            Name = "block" + blockCounter.ToString(),
                            Size = new Size(8, 8),
                            Location = new Point(0 + 8 * j, 0 + 8 * i),
                            Image = imgarray[blockCounter],
                            Visible = true,

                        };
                        picture.MouseClick += setBlock_MouseClick;
                        picture.MouseMove += setBlock_MouseMove;
                        picture.MouseLeave += setBlock_MouseLeave;
                        panel1.Controls.Add(picture);
                        blockCounter++;
                    }
                }
            }
        }

        private void tl2CreateBlocks()
        {
            int hgt = tl2Tileset.Height;
            int filas = hgt / 8;
            int totalBlocks = filas * 16;
            int tl2BlockCounter = 0;

            if (blockCounter > 0)
            {
                PictureBox picBlock = this.Controls.OfType<PictureBox>().FirstOrDefault(x => x.Name == "blockSelected");
                if (picBlock != null)
                {
                    this.Controls.Remove(picBlock);
                }
                panel1.Visible = false;
                //panel1.Controls.Clear();
            }

            var imgarray = new Image[totalBlocks];
            if (tl2Tileset != null)
            {
                setColorTransparency(selectedPalette, 0, false);
                //MessageBox.Show("blockCounter: " + blockCounter.ToString());
                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        var img = tl2Tileset;
                        imgarray[tl2BlockCounter] = new Bitmap(8, 8);
                        var graphics = Graphics.FromImage(imgarray[tl2BlockCounter]);
                        graphics.DrawImage(img, new Rectangle(0, 0, 8, 8), new Rectangle(j * 8, i * 8, 8, 8), GraphicsUnit.Pixel);
                        graphics.Dispose();

                        var picture = new PictureBox
                        {
                            Name = "block" + (blockCounter + tl2BlockCounter).ToString(),
                            Size = new Size(8, 8),
                            Location = new Point(0 + 8 * j, + 8 * i + tileset.Height),
                            Image = imgarray[tl2BlockCounter],
                            Visible = true,

                        };
                        //MessageBox.Show("block" + (blockCounter + tl2BlockCounter).ToString());
                        picture.MouseClick += setBlock_MouseClick;
                        picture.MouseMove += setBlock_MouseMove;
                        picture.MouseLeave += setBlock_MouseLeave;
                        panel1.Controls.Add(picture);
                        tl2BlockCounter++;
                    }
                }
                for (int i = 0; i < tl2BlockCounter; i++)
                {
                    PictureBox picBlock = panel1.Controls.OfType<PictureBox>().FirstOrDefault(a => a.Name == "block" + (i +blockCounter).ToString());
                    if (picBlock != null)
                    {
                        //MessageBox.Show("block" + (i + blockCounter).ToString() + " Location: " + picBlock.Location);
                    } else
                    {
                        //MessageBox.Show("No existe");
                    }

                }
            }
        }

        private void resetFlipSystem() {
            xFlipped = false;
            yFlipped = false;
            if (checkBox1.Checked)
            {
                checkBox1.Checked = false;
                PictureBox picBlock = this.Controls.OfType<PictureBox>().FirstOrDefault(a => a.Name == "blockSelected");
                if (picBlock != null)
                {
                    Image flipImage = picBlock.Image;
                    flipImage.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    picBlock.Image = flipImage;
                }
            }
            if (checkBox2.Checked)
            {
                checkBox2.Checked = false;
                PictureBox picBlock = this.Controls.OfType<PictureBox>().FirstOrDefault(a => a.Name == "blockSelected");
                if (picBlock != null)
                {
                    Image flipImage = picBlock.Image;
                    flipImage.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    picBlock.Image = flipImage;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void debuggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int maxFolderValue = 0;
            int securityCopiesAmount = 10;
            string newFolderValue = "";

            if (Directory.Exists(Application.StartupPath + "\\seccp\\") == false)
            {
                Directory.CreateDirectory(Application.StartupPath + "\\seccp");
            }

            for (int i = 0; i < (securityCopiesAmount - 1); i++)
            {
                string stringFolderValue = maxFolderValue.ToString();

                if (stringFolderValue.Length == 1)
                {
                    stringFolderValue = "0" + stringFolderValue;
                }

                if (Directory.Exists(Application.StartupPath + "\\seccp\\" + stringFolderValue))
                {
                    //MessageBox.Show("\\seccp\\" + stringFolderValue + " existe");
                    maxFolderValue++;
                } else
                {
                    //MessageBox.Show("\\seccp\\" + stringFolderValue + " NO existe");
                }
            }

            if (maxFolderValue == (securityCopiesAmount - 1))
            {
                string stringFolderValue = maxFolderValue.ToString();
                if (stringFolderValue.Length == 1)
                {
                    stringFolderValue = "0" + stringFolderValue;
                }
                if (Directory.Exists(Application.StartupPath + "\\seccp\\" + stringFolderValue))
                {
                    Directory.Delete(Application.StartupPath + "\\seccp\\" + stringFolderValue, true);
                }
                
            }

            if (maxFolderValue.ToString().Length == 1)
            {
                newFolderValue = "0" + maxFolderValue;
            } else
            {
                newFolderValue = maxFolderValue.ToString();
            }

            Directory.CreateDirectory(Application.StartupPath + "\\seccp\\" + newFolderValue);

            for (int i = maxFolderValue; i > 0; i--)
            {
                string stringFolderValue = i.ToString();
                string stringFolderValueMinOne = (i - 1).ToString();
                //MessageBox.Show(stringFolderValue);

                if (stringFolderValue.Length == 1)
                {
                    stringFolderValue = "0" + stringFolderValue;
                }
                if (stringFolderValueMinOne.Length == 1)
                {
                    stringFolderValueMinOne = "0" + stringFolderValueMinOne;
                }
                //MessageBox.Show(stringFolderValue);
                if (Directory.Exists(Application.StartupPath + "\\seccp\\" + stringFolderValue))
                {
                    Directory.Delete(Application.StartupPath + "\\seccp\\" + stringFolderValue, true);
                }
                System.IO.Directory.Move((Application.StartupPath + "\\seccp\\" + stringFolderValueMinOne), (Application.StartupPath + "\\seccp\\" + stringFolderValue));
            }
            if (Directory.Exists(Application.StartupPath + "\\seccp\\00") == false)
            {
                Directory.CreateDirectory(Application.StartupPath + "\\seccp\\00");
            }


            //MessageBox.Show(Application.StartupPath);
            //Directory.CreateDirectory(Application.StartupPath + "\\test");
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void checkBox1_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            PictureBox picBlock = this.Controls.OfType<PictureBox>().FirstOrDefault(a => a.Name == "blockSelected");
            if (picBlock != null)
            {
                //picBlock = picBlock.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                Image flipImage = picBlock.Image;
                flipImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                picBlock.Image = flipImage;
                if (checkBox1.Checked){xFlipped = true;}else{xFlipped = false;}


            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            PictureBox picBlock = this.Controls.OfType<PictureBox>().FirstOrDefault(a => a.Name == "blockSelected");
            if (picBlock != null)
            {
                //picBlock = picBlock.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                Image flipImage = picBlock.Image;
                flipImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                picBlock.Image = flipImage;
                if (checkBox2.Checked) { yFlipped = true; } else { yFlipped = false; }
            }
        }

        public string returnZero(int num)
        {
            if (num < 10)
                return "0";
            else
                return null;
        }

        private void changePalette(int palette) {
            //MessageBox.Show(palettesLoaded.ToString());
            for (int i = 0; i < palettesLoaded; i++)
            {
                setColorTransparency(i, 0, false);
            }
            var bitmap = new Bitmap(128, 256, PixelFormat.Format4bppIndexed);
            var colorPalette = bitmap.Palette;
            var colorEntries = colorPalette.Entries;
            tileset.Palette = palettes[palette];
            if (tl2Tileset != null) { tl2Tileset.Palette = palettes[palette]; }
            selectedPalette = palette;
            createBlocks();
            if (tl2Tileset != null)
            {
                tl2CreateBlocks();
            }
            resetFlipSystem();
            panel1.Visible = true;

            //loadBlockTiles();
        }

        private void setPalette(int palette)
        {
            var bitmap = new Bitmap(128, 256, PixelFormat.Format4bppIndexed);
            var colorPalette = bitmap.Palette;
            var colorEntries = colorPalette.Entries;
            tileset.Palette = palettes[palette];
            if (tl2Tileset != null) { tl2Tileset.Palette = palettes[palette]; }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex) {
                case 0:
                    changePalette(0);
                    break;
                case 1:
                    changePalette(1);
                    break;
                case 2:
                    changePalette(2);
                    break;
                case 3:
                    changePalette(3);
                    break;
                case 4:
                    changePalette(4);
                    break;
                case 5:
                    changePalette(5);
                    break;
                case 6:
                    changePalette(6);
                    break;
                case 7:
                    changePalette(7);
                    break;
                case 8:
                    changePalette(8);
                    break;
                case 9:
                    changePalette(9);
                    break;
                case 10:
                    changePalette(10);
                    break;
                case 11:
                    changePalette(11);
                    break;
                case 12:
                    changePalette(12);
                    break;
                case 13:
                    changePalette(13);
                    break;
                case 14:
                    changePalette(14);
                    break;
                case 15:
                    changePalette(15);
                    break;

            }
        }

        private void abrirArchivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();

            if (FBD.ShowDialog() == DialogResult.OK)
            {
                directory = FBD.SelectedPath;
                if (Directory.Exists(Directory.GetDirectories(directory, "palettes").FirstOrDefault()))
                {
                    dirPalettes = Directory.GetDirectories(directory, "palettes").FirstOrDefault();
                    int fileCount = Directory.GetFiles(dirPalettes, "*.pal").Length;

                    if (fileCount > 7)
                    {
                        if (tl2DirPalettes != null)
                        {
                            fileCount = 8;
                        }
                    }

                    if (fileCount > 0)
                    {

                        if (File.Exists(directory.ToString() + "\\metatiles.bin"))
                        {
                            binFile = directory.ToString() + "\\metatiles.bin";
                            FileInfo fi = new FileInfo(binFile);
                            bytesToLoad = Int32.Parse((fi.Length).ToString());

                            if (File.Exists(directory.ToString() + "\\metatile_attributes.bin"))
                            {
                                binAttributeFile = directory.ToString() + "\\metatile_attributes.bin";
                                FileInfo fiAttribute = new FileInfo(binAttributeFile);
                                bytesToLoadAttribute = Int32.Parse((fiAttribute.Length).ToString());

                                if (File.Exists(directory.ToString() + "\\tiles.bmp"))
                                {
                                    hideAll();

                                    string pathToSearch = binFile;
                                    pathToSearch = pathToSearch.Substring(0, pathToSearch.LastIndexOfAny(new char[] { '\\', '/' }));
                                    pathToSearch = pathToSearch.Substring(pathToSearch.LastIndexOfAny(new char[] { '\\', '/' }) + 1);
                                    //MessageBox.Show(pathToSearch);

                                    if (Directory.Exists(Application.StartupPath + "\\seccp\\00") == false)
                                    {
                                        Directory.CreateDirectory(Application.StartupPath + "\\seccp\\00");
                                    }
                                    if (Directory.Exists(Application.StartupPath + "\\seccp\\00\\" + pathToSearch) == false)
                                    {
                                        Directory.CreateDirectory(Application.StartupPath + "\\seccp\\00\\" + pathToSearch);
                                        File.Copy(binFile, Application.StartupPath + "\\seccp\\00\\" + pathToSearch + "\\metatiles.bin");
                                        File.Copy(binAttributeFile, Application.StartupPath + "\\seccp\\00\\" + pathToSearch + "\\metatile_attributes.bin");
                                    }
                                    

                                    pngFile = directory.ToString() + "\\tiles.bmp";
                                    Bitmap s = new Bitmap(@pngFile);

                                    tileset = s.Clone(new Rectangle(0, 0, s.Width, s.Height), PixelFormat.Format4bppIndexed);

                                    var bitmap = new Bitmap(128, 256, PixelFormat.Format4bppIndexed);

                                    comboBox1.Items.Clear();
                                    comboBox4.Items.Clear();
                                    palettes = new ColorPalette[16];
                                    palettesLoaded = 0;

                                    //MessageBox.Show("fileCount: " + fileCount.ToString());

                                    for (int j = 0; j < fileCount; j++)
                                    {

                                        string[] paletteLines = System.IO.File.ReadAllLines(dirPalettes + "\\" + returnZero(j) + j.ToString() + ".pal");

                                        using (Bitmap loadedImage = new Bitmap(128, 256, PixelFormat.Format4bppIndexed))
                                        {

                                            ColorPalette pal = loadedImage.Palette;

                                            for (Int32 i = 0; i < pal.Entries.Length; i++)
                                            {
                                                string[] colors = paletteLines[i + 3].Split();
                                                pal.Entries[i] = Color.FromArgb(Int32.Parse(colors[0]), Int32.Parse(colors[1]), Int32.Parse(colors[2]));
                                                //richTextBox1.Text += colors[0] + " " + colors[1] + " " + colors[2] + " --- ";
                                            }
                                            palettes[j] = pal;
                                            //MessageBox.Show("Color 0, Paleta " + j + ": " + palettes[j].Entries[0].ToString());
                                        }
                                        comboBox1.Items.Insert(j, "Paleta " + j.ToString());
                                        palettesLoaded++;
                                    }

                                    if (tl2DirPalettes != null)
                                    {
                                        int tl2FileCount = Directory.GetFiles(tl2DirPalettes, "*.pal").Length;

                                        if (tl2FileCount > 4)
                                        {
                                            tl2FileCount = 5;
                                        }

                                        for (int j = 8; j < (8 + tl2FileCount); j++)
                                        {

                                            string[] tl2PaletteLines = System.IO.File.ReadAllLines(tl2DirPalettes + "\\" + returnZero(j) + j.ToString() + ".pal");

                                            using (Bitmap tl2LoadedImage = new Bitmap(128, 256, PixelFormat.Format4bppIndexed))
                                            {

                                                ColorPalette tl2Pal = tl2LoadedImage.Palette;
                                                for (Int32 i = 0; i < tl2Pal.Entries.Length; i++)
                                                {
                                                    string[] tl2Colors = tl2PaletteLines[i + 3].Split();
                                                    tl2Pal.Entries[i] = Color.FromArgb(Int32.Parse(tl2Colors[0]), Int32.Parse(tl2Colors[1]), Int32.Parse(tl2Colors[2]));
                                                    //richTextBox1.Text += colors[0] + " " + colors[1] + " " + colors[2] + " --- ";
                                                }
                                                palettes[j] = tl2Pal;
                                                //MessageBox.Show("Color 0, Paleta " + j + ": " + palettes[j].Entries[0].ToString());
                                            }
                                            comboBox1.Items.Insert(j, "Paleta " + j.ToString());
                                            palettesLoaded++;
                                        }
                                    }
                                    setPalette(0);
                                    comboBox1.SelectedIndex = 0;
                                    comboBox4.Items.Insert(0, "metatiles.bin (primario)");
                                    comboBox4.Items.Insert(1, "metatiles_attributes.bin (primario)");
                                    comboBox4.SelectedIndex = 0;
                                    if (tileset != null)
                                    {
                                        createBlocks();

                                        blockPicked = 0;
                                        label1.Text = "Tile: " + blockPicked.ToString() + "00";
                                        createNewBlock(0);
                                        resetFlipSystem();
                                    }
                                    reloadFile();
                                    reloadFileAttribute();
                                    setAttributeNames();
                                    loadBlockTiles();
                                    setColorTransparency(0, 0, false);
                                    changePalette(0);
                                    vistaProfesionalToolStripMenuItem.Enabled = true;
                                    importarTilesetSecundarioToolStripMenuItem.Enabled = true;
                                    vaciarTilesetPrincipalToolStripMenuItem.Enabled = true;
                                    PictureBox callSetBlock = panel2.Controls.OfType<PictureBox>().FirstOrDefault(x => x.Name == "blockTile0");
                                    if (callSetBlock != null)
                                    {
                                        setTileBlock_MouseClick(callSetBlock, null);
                                    }
                                    PictureBox callSetTile = panel1.Controls.OfType<PictureBox>().FirstOrDefault(x => x.Name == "block0");
                                    if (callSetTile != null)
                                    {
                                        setBlock_MouseClick(callSetTile, null);
                                    }
                                    showAll();
                                }
                                else
                                {
                                    MessageBox.Show("The program couldn't find the \"tiles.bmp\" file inside the selected folder.\nPlease make sure that the file \"tiles\" exists and that it's a .bmp image file.",
                                    "Error: Couldn't find the file.",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button1);
                                }
                            } else
                            {
                                MessageBox.Show("The program couldn't find the \"metatile_attributes.bin\" file inside the selected folder.\nPlease make sure that the \"metatile_attributes\" file exists and that it's a .bin file.",
                                "Error: Couldn't find the file.",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);
                            }
                        }
                        else
                        {
                            MessageBox.Show("The program couldn't find the \"metatiles.bin\" file inside the selected folder.\nPlease make sure that the \"metatiles\" file exists and that it's a .bin file.",
                            "Error: Couldn't find the file.",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The program couldn't find any \".pal\" files inside the \"palettes\" folder.\nPlease make sure that there's one or more files named:\n\"00.pal\"\n\"01.pal\"\n\"02.pal\"\n...",
                        "Error: Couldn't find a file.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    MessageBox.Show("The program couldn't find the \"palettes\" folder inside the chosen directory.\nPlease make sure that the \"palettes\" folder exists.",
                    "Error: Couldn't find a folder.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show("You didn't choose a folder",
                "Error: No folders selected.",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
        }

        private void importarTilesetSecundarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBDSecondary = new FolderBrowserDialog();

            if (FBDSecondary.ShowDialog() == DialogResult.OK)
            {
                tl2Directory = FBDSecondary.SelectedPath;
                if (Directory.Exists(Directory.GetDirectories(tl2Directory, "palettes").FirstOrDefault()))
                {
                    tl2DirPalettes = Directory.GetDirectories(tl2Directory, "palettes").FirstOrDefault();
                    int fileCount = Directory.GetFiles(tl2DirPalettes, "*.pal").Length;

                    if (fileCount > 4)
                    {
                        fileCount = 5;
                    }

                    if (fileCount > 0)
                    {

                        if (File.Exists(tl2Directory.ToString() + "\\metatiles.bin"))
                        {
                            tl2BinFile = tl2Directory.ToString() + "\\metatiles.bin";
                            FileInfo tl2Fi = new FileInfo(tl2BinFile);
                            tl2BytesToLoad = tl2Fi.Length;

                            if (File.Exists(tl2Directory.ToString() + "\\metatile_attributes.bin"))
                            {
                                tl2BinAttributeFile = tl2Directory.ToString() + "\\metatile_attributes.bin";
                                FileInfo tl2FiAttribute = new FileInfo(tl2BinAttributeFile);
                                tl2BytesToLoadAttribute = tl2FiAttribute.Length;

                                if (File.Exists(tl2Directory.ToString() + "\\tiles.bmp"))
                                {
                                    hideAll();


                                    string pathToSearch = tl2BinFile;
                                    pathToSearch = pathToSearch.Substring(0, pathToSearch.LastIndexOfAny(new char[] { '\\', '/' }));
                                    pathToSearch = pathToSearch.Substring(pathToSearch.LastIndexOfAny(new char[] { '\\', '/' }) + 1);
                                    //MessageBox.Show(pathToSearch);

                                    if (Directory.Exists(Application.StartupPath + "\\seccp\\00") == false)
                                    {
                                        Directory.CreateDirectory(Application.StartupPath + "\\seccp\\00");
                                    }
                                    if (Directory.Exists(Application.StartupPath + "\\seccp\\00\\" + pathToSearch) == false)
                                    {
                                        Directory.CreateDirectory(Application.StartupPath + "\\seccp\\00\\" + pathToSearch);
                                        File.Copy(tl2BinFile, Application.StartupPath + "\\seccp\\00\\" + pathToSearch + "\\metatiles.bin");
                                        File.Copy(tl2BinAttributeFile, Application.StartupPath + "\\seccp\\00\\" + pathToSearch + "\\metatile_attributes.bin");
                                    }

                                    tl2PngFile = tl2Directory.ToString() + "\\tiles.bmp";
                                    Bitmap tl2S = new Bitmap(@tl2PngFile);

                                    tl2Tileset = tl2S.Clone(new Rectangle(0, 0, tl2S.Width, tl2S.Height), PixelFormat.Format4bppIndexed);

                                    var tl2Bitmap = new Bitmap(128, 256, PixelFormat.Format4bppIndexed);

                                    var tl2ColorPalette = tl2Bitmap.Palette;
                                    var colorEntries = tl2ColorPalette.Entries;

                                    //REVISAR FUNCIONAMIENTO DE LAS PALETAS
                                    comboBox1.Items.Clear();
                                    comboBox4.Items.Clear();
                                    palettes = new ColorPalette[16];
                                    palettesLoaded = 0;

                                    int tl1FileCount = Directory.GetFiles(dirPalettes, "*.pal").Length;

                                    if (tl1FileCount > 7)
                                    {
                                        tl1FileCount = 8;
                                    }

                                    for (int j = 0; j < tl1FileCount; j++)
                                    {

                                        string[] paletteLines = System.IO.File.ReadAllLines(dirPalettes + "\\" + returnZero(j) + j.ToString() + ".pal");

                                        using (Bitmap loadedImage = new Bitmap(128, 256, PixelFormat.Format4bppIndexed))
                                        {

                                            ColorPalette pal = loadedImage.Palette;

                                            for (Int32 i = 0; i < pal.Entries.Length; i++)
                                            {
                                                string[] colors = paletteLines[i + 3].Split();
                                                pal.Entries[i] = Color.FromArgb(Int32.Parse(colors[0]), Int32.Parse(colors[1]), Int32.Parse(colors[2]));
                                                //richTextBox1.Text += colors[0] + " " + colors[1] + " " + colors[2] + " --- ";
                                                //MessageBox.Show("Paleta " + j.ToString() + ", color " + i.ToString() + ": " + pal.Entries[i].ToString());
                                            }
                                            palettes[j] = pal;
                                            //MessageBox.Show("Color 0, Paleta " + j + ": " + palettes[j].Entries[0].ToString());
                                        }
                                        comboBox1.Items.Insert(j, "Paleta " + j.ToString());
                                        palettesLoaded++;
                                    }

                                    //MessageBox.Show("Palette 1, second color: " + palettes[1].Entries[4].ToString() + "\nPalette 6, second color: " + palettes[6].Entries[4].ToString());

                                    for (int j = 8; j < (8 + fileCount); j++)
                                    {

                                        string[] tl2PaletteLines = System.IO.File.ReadAllLines(tl2DirPalettes + "\\" + returnZero(j) + j.ToString() + ".pal");

                                        using (Bitmap tl2LoadedImage = new Bitmap(128, 256, PixelFormat.Format4bppIndexed))
                                        {

                                            ColorPalette tl2Pal = tl2LoadedImage.Palette;
                                            for (Int32 i = 0; i < tl2Pal.Entries.Length; i++)
                                            {
                                                string[] tl2Colors = tl2PaletteLines[i + 3].Split();
                                                tl2Pal.Entries[i] = Color.FromArgb(Int32.Parse(tl2Colors[0]), Int32.Parse(tl2Colors[1]), Int32.Parse(tl2Colors[2]));
                                            }
                                            palettes[j] = tl2Pal;
                                        }
                                        comboBox1.Items.Insert(j, "Paleta " + j.ToString());
                                        palettesLoaded++;
                                    }

                                    setPalette(0);
                                    comboBox1.SelectedIndex = 0;
                                    comboBox4.Items.Insert(0, "metatiles.bin (primario)");
                                    comboBox4.Items.Insert(1, "metatiles_attributes.bin (primario)");
                                    comboBox4.Items.Insert(2, "metatiles.bin (secundario)");
                                    comboBox4.Items.Insert(3, "metatiles_attributes.bin (secundario)");
                                    comboBox4.SelectedIndex = 0;
                                    if (tileset != null)
                                    {
                                        tl2CreateBlocks();
                                        blockPicked = 0;
                                        label1.Text = "Tile: " + blockPicked.ToString() + "00";
                                        createNewBlock(0);
                                        resetFlipSystem();
                                    }
                                    reloadFile();
                                    reloadFileAttribute();
                                    setAttributeNames();
                                    loadBlockTiles();
                                    setColorTransparency(0, 0, false);
                                    changePalette(0);
                                    vistaProfesionalToolStripMenuItem.Enabled = true;
                                    vaciarTilesetSecundarioToolStripMenuItem.Enabled = true;
                                    PictureBox callSetBlock = panel2.Controls.OfType<PictureBox>().FirstOrDefault(x => x.Name == "blockTile0");
                                    if (callSetBlock != null)
                                    {
                                        setTileBlock_MouseClick(callSetBlock, null);
                                    }
                                    PictureBox callSetTile = panel1.Controls.OfType<PictureBox>().FirstOrDefault(x => x.Name == "block0");
                                    if (callSetTile != null)
                                    {
                                        setBlock_MouseClick(callSetTile, null);
                                    }
                                    showAll();
                                }
                                else
                                {
                                    MessageBox.Show("The program couldn't find the \"tiles.bmp\" file inside the selected folder.\nPlease make sure that the file \"tiles\" exists and that it's a .bmp image file.",
                                    "Error: Couldn't find the file.",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button1);
                                }
                            }
                            else
                            {
                                MessageBox.Show("The program couldn't find the \"metatile_attributes.bin\" file inside the selected folder.\nPlease make sure that the \"metatile_attributes\" file exists and that it's a .bin file.",
                                "Error: Couldn't find the file.",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1);
                            }
                        }
                        else
                        {
                            MessageBox.Show("The program couldn't find the \"metatiles.bin\" file inside the selected folder.\nPlease make sure that the \"metatiles\" file exists and that it's a .bin file.",
                            "Error: Couldn't find the file.",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The program couldn't find any \".pal\" files inside the \"palettes\" folder.\nPlease make sure that there's one or more files named:\n\"00.pal\"\n\"01.pal\"\n\"02.pal\"\n...",
                        "Error: Couldn't find a file.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    MessageBox.Show("The program couldn't find the \"palettes\" folder inside the chosen directory.\nPlease make sure that the \"palettes\" folder exists.",
                    "Error: Couldn't find a folder.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show("You didn't choose a folder",
                "Error: No folders selected.",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
            }
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (vistaProfesionalToolStripMenuItem.Checked == true)
            {
                vistaProfesionalToolStripMenuItem.Checked = false;
                normalToolStripMenuItem.Checked = true;
                setSmallSize();
            }
        }

        private void vistaProfesionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (normalToolStripMenuItem.Checked == true)
            {
                normalToolStripMenuItem.Checked = false;
                vistaProfesionalToolStripMenuItem.Checked = true;
                setHighSize();
            }
        }

        private void vistaProfesionalToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (normalToolStripMenuItem.Checked == false && vistaProfesionalToolStripMenuItem.Checked == false)
            {
                vistaProfesionalToolStripMenuItem.Checked = true;
                setHighSize();
            }
        }

        private void normalToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (normalToolStripMenuItem.Checked == false && vistaProfesionalToolStripMenuItem.Checked == false)
            {
                normalToolStripMenuItem.Checked = true;
                setSmallSize();
            }
        }

        private void setHighSize()
        {
            tabControl1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            button4.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            comboBox4.Visible = true;
            this.Size = new Size(1020, this.Height);
            //this.Text = "IKBlockEditor - Editor de Bloques (Vista avanzada)";
        }

        private void setSmallSize()
        {
            this.Size = new Size(574, this.Height);
            tabControl1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            button4.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            comboBox4.Visible = false;
            //this.Text = "IKBlockEditor - Editor de Bloques (Vista normal)";
        }

        private void setAttributeNames()
        {
            //ComboBox2
            comboBox2.Items.Insert(0, "00: NORMAL");
            comboBox2.Items.Insert(1, "01: SECRET BASE WALL");
            comboBox2.Items.Insert(2, "02: TALL GRASS");
            comboBox2.Items.Insert(3, "03: LONG GRASS");
            comboBox2.Items.Insert(4, "04: UNUSED 04");
            comboBox2.Items.Insert(5, "05: UNUSED 05");
            comboBox2.Items.Insert(6, "06: UNUSED DEEP SAND");
            comboBox2.Items.Insert(7, "07: SHORT GRASS");
            comboBox2.Items.Insert(8, "08: UNUSED CAVE");
            comboBox2.Items.Insert(9, "09: LONG GRASS SOUTH EDGE");
            comboBox2.Items.Insert(10, "0A: NO RUNNING");
            comboBox2.Items.Insert(11, "0B: INDOOR ENCOUNTER");
            comboBox2.Items.Insert(12, "0C: MOUNTAIN TOP");
            comboBox2.Items.Insert(13, "0D: SECRET BASE GLITTER MATTTLE PYRAMID WARP");
            comboBox2.Items.Insert(14, "0E: MOSSDEEP GYM WARP");
            comboBox2.Items.Insert(15, "0F: MT PYRE HOLE");
            comboBox2.Items.Insert(16, "10: POND WATER");
            comboBox2.Items.Insert(17, "11: SEMI DEEP WATER");
            comboBox2.Items.Insert(18, "12: UNUSED DEEP WATER");
            comboBox2.Items.Insert(19, "13: WATERFALL");
            comboBox2.Items.Insert(20, "14: SOOTOPOLIS DEEP WATER");
            comboBox2.Items.Insert(21, "15: OCEAN WATER");
            comboBox2.Items.Insert(22, "16: PUDDLE");
            comboBox2.Items.Insert(23, "17: SHALLOW WATER");
            comboBox2.Items.Insert(24, "18: UNUSED SOOTOPOLIS DEEP WATER");
            comboBox2.Items.Insert(25, "19: NO SURFACING");
            comboBox2.Items.Insert(26, "1A: UNUSED SOOTOPOLIS DEEP WATER 2");
            comboBox2.Items.Insert(27, "1B: STAIRS OUTSIDE ABANDONED SHIP");
            comboBox2.Items.Insert(28, "1C: SHOAL CAVE ENTRANCE");
            comboBox2.Items.Insert(29, "1D: UNUSED 1D");
            comboBox2.Items.Insert(30, "1E: UNUSED 1E");
            comboBox2.Items.Insert(31, "1F: UNUSED 1F");
            comboBox2.Items.Insert(32, "20: ICE");
            comboBox2.Items.Insert(33, "21: SAND");
            comboBox2.Items.Insert(34, "22: SEAWEED");
            comboBox2.Items.Insert(35, "23: UNUSED 23");
            comboBox2.Items.Insert(36, "24: ASHGRASS");
            comboBox2.Items.Insert(37, "25: FOOTPRINTS");
            comboBox2.Items.Insert(38, "26: THIN ICE");
            comboBox2.Items.Insert(39, "27: CRACKED ICE");
            comboBox2.Items.Insert(40, "28: HOT SPRINGS");
            comboBox2.Items.Insert(41, "29: LAVARIDGE GYM B1F WARP");
            comboBox2.Items.Insert(42, "2A: SEAWEED NO SURFACING");
            comboBox2.Items.Insert(43, "2B: REFLECTION UNDER BRIDGE");
            comboBox2.Items.Insert(44, "2C: UNUSED 2C");
            comboBox2.Items.Insert(45, "2D: UNUSED 2D");
            comboBox2.Items.Insert(46, "2E: UNUSED 2E");
            comboBox2.Items.Insert(47, "2F: UNUSED 2F");
            comboBox2.Items.Insert(48, "30: IMPASSABLE EAST");
            comboBox2.Items.Insert(49, "31: IMPASSABLE WEST");
            comboBox2.Items.Insert(50, "32: IMPASSABLE NORTH");
            comboBox2.Items.Insert(51, "33: IMPASSABLE SOUTH");
            comboBox2.Items.Insert(52, "34: IMPASSABLE NORTHEAST");
            comboBox2.Items.Insert(53, "35: IMPASSABLE NORTHWEST");
            comboBox2.Items.Insert(54, "36: IMPASSABLE SOUTHEAST");
            comboBox2.Items.Insert(55, "37: IMPASSABLE SOUTHWEST");
            comboBox2.Items.Insert(56, "38: JUMP EAST");
            comboBox2.Items.Insert(57, "39: JUMP WEST");
            comboBox2.Items.Insert(58, "3A: JUMP NORTH");
            comboBox2.Items.Insert(59, "3B: JUMP SOUTH");
            comboBox2.Items.Insert(60, "3C: JUMP NORTHEAST");
            comboBox2.Items.Insert(61, "3D: JUMP NORTHWEST");
            comboBox2.Items.Insert(62, "3E: JUMP SOUTHEAST");
            comboBox2.Items.Insert(63, "3F: JUMP SOUTHWEST");
            comboBox2.Items.Insert(64, "40: WALK EAST");
            comboBox2.Items.Insert(65, "41: WALK WEST");
            comboBox2.Items.Insert(66, "42: WALK NORTH");
            comboBox2.Items.Insert(67, "43: WALK SOUTH");
            comboBox2.Items.Insert(68, "44: SLIDE EAST");
            comboBox2.Items.Insert(69, "45: SLIDE WEST");
            comboBox2.Items.Insert(70, "46: SLIDE NORTH");
            comboBox2.Items.Insert(71, "47: SLIDE SOUTH");
            comboBox2.Items.Insert(72, "48: TRICK HOUSE PUZZLE 8 FLOOR");
            comboBox2.Items.Insert(73, "49: UNUSED 49");
            comboBox2.Items.Insert(74, "4A: UNUSED 4A");
            comboBox2.Items.Insert(75, "4B: UNUSED 4B");
            comboBox2.Items.Insert(76, "4C: UNUSED 4C");
            comboBox2.Items.Insert(77, "4D: UNUSED 4D");
            comboBox2.Items.Insert(78, "4E: UNUSED 4E");
            comboBox2.Items.Insert(79, "4F: UNUSED 4F");
            comboBox2.Items.Insert(80, "50: UNUSED EASTWARD CURRENT");
            comboBox2.Items.Insert(81, "51: WESTWARD CURRENT");
            comboBox2.Items.Insert(82, "52: NORTHWARD CURRENT");
            comboBox2.Items.Insert(83, "53: SOUTHWARD CURRENT");
            comboBox2.Items.Insert(84, "54: UNUSED 54");
            comboBox2.Items.Insert(85, "55: UNUSED 55");
            comboBox2.Items.Insert(86, "56: UNUSED 56");
            comboBox2.Items.Insert(87, "57: UNUSED 57");
            comboBox2.Items.Insert(88, "58: UNUSED 58");
            comboBox2.Items.Insert(89, "59: UNUSED 59");
            comboBox2.Items.Insert(90, "5A: UNUSED 5A");
            comboBox2.Items.Insert(91, "5B: UNUSED 5B");
            comboBox2.Items.Insert(92, "5C: UNUSED 5C");
            comboBox2.Items.Insert(93, "5D: UNUSED 5D");
            comboBox2.Items.Insert(94, "5E: UNUSED 5E");
            comboBox2.Items.Insert(95, "5F: UNUSED 5F");
            comboBox2.Items.Insert(96, "60: NON ANIMATED DOOR");
            comboBox2.Items.Insert(97, "61: LADDER");
            comboBox2.Items.Insert(98, "62: UNUSED EAST ARROW WARP");
            comboBox2.Items.Insert(99, "63: WEST ARROW WARP");
            comboBox2.Items.Insert(100, "64: NORTH ARROW WARP");
            comboBox2.Items.Insert(101, "65: SOUTH ARROW WARP");
            comboBox2.Items.Insert(102, "66: CRACKED FLOOR HOLE");
            comboBox2.Items.Insert(103, "67: AQUA HIDEOUT WARP");
            comboBox2.Items.Insert(104, "68: LAVARIDGE GYM 1F WARP");
            comboBox2.Items.Insert(105, "69: ANIMATED DOOR");
            comboBox2.Items.Insert(106, "6A: UP ESCALATOR");
            comboBox2.Items.Insert(107, "6B: DOWN ESCALATOR");
            comboBox2.Items.Insert(108, "6C: WATER DOOR");
            comboBox2.Items.Insert(109, "6D: WATER SOUTH ARROW WARP");
            comboBox2.Items.Insert(110, "6E: UNUSED DEEP SOUTH WARP");
            comboBox2.Items.Insert(111, "6F: UNUSED 6F");
            comboBox2.Items.Insert(112, "70: WARP OR BRIDGE");
            comboBox2.Items.Insert(113, "71: UNUSED 71");
            comboBox2.Items.Insert(114, "72: ROUTE120 NORTH BRIDGE 1");
            comboBox2.Items.Insert(115, "73: ROUTE120 NORTH BRIDGE 2");
            comboBox2.Items.Insert(116, "74: PACIFIDLOG VERTICAL LOG 1");
            comboBox2.Items.Insert(117, "75: PACIFIDLOG VERTICAL LOG 2");
            comboBox2.Items.Insert(118, "76: PACIFIDLOG HORIZONTAL LOG 1");
            comboBox2.Items.Insert(119, "77: PACIFIDLOG HORIZONTAL LOG 2");
            comboBox2.Items.Insert(120, "78: FORTREE BRIDGE");
            comboBox2.Items.Insert(121, "79: UNUSED 79");
            comboBox2.Items.Insert(122, "7A: ROUTE120 SOUTH BRIDGE 1");
            comboBox2.Items.Insert(123, "7B: ROUTE120 SOUTH BRIDGE 2");
            comboBox2.Items.Insert(124, "7C: ROUTE120 NORTH BRIDGE 3");
            comboBox2.Items.Insert(125, "7D: ROUTE120 NORTH BRIDGE 4");
            comboBox2.Items.Insert(126, "7E: UNUSED 7E");
            comboBox2.Items.Insert(127, "7F: ROUTE110 BRIDGE");
            comboBox2.Items.Insert(128, "80: COUNTER");
            comboBox2.Items.Insert(129, "81: UNUSED 81");
            comboBox2.Items.Insert(130, "82: UNUSED 82");
            comboBox2.Items.Insert(131, "83: PC");
            comboBox2.Items.Insert(132, "84: LINK BATTLE RECORDS");
            comboBox2.Items.Insert(133, "85: REGION MAP");
            comboBox2.Items.Insert(134, "86: TELEVISION");
            comboBox2.Items.Insert(135, "87: POKEBLOCK FEEDER");
            comboBox2.Items.Insert(136, "88: UNUSED 88");
            comboBox2.Items.Insert(137, "89: SLOT MACHINE");
            comboBox2.Items.Insert(138, "8A: ROULETTE");
            comboBox2.Items.Insert(139, "8B: CLOSED SOOTOPOLIS DOOR");
            comboBox2.Items.Insert(140, "8C: TRICK HOUSE PUZZLE DOOR");
            comboBox2.Items.Insert(141, "8D: PETALBURG GYM DOOR");
            comboBox2.Items.Insert(142, "8E: RUNNING SHOES MANUAL");
            comboBox2.Items.Insert(143, "8F: QUESTIONNAIRE");
            comboBox2.Items.Insert(144, "90: SECRET BASE SPOT RED CAVE");
            comboBox2.Items.Insert(145, "91: SECRET BASE SPOT RED CAVE OPEN");
            comboBox2.Items.Insert(146, "92: SECRET BASE SPOT BROWN CAVE");
            comboBox2.Items.Insert(147, "93: SECRET BASE SPOT BROWN CAVE OPEN");
            comboBox2.Items.Insert(148, "94: SECRET BASE SPOT YELLOW CAVE");
            comboBox2.Items.Insert(149, "95: SECRET BASE SPOT YELLOW CAVE OPEN");
            comboBox2.Items.Insert(150, "96: SECRET BASE SPOT TREE 1");
            comboBox2.Items.Insert(151, "97: SECRET BASE SPOT TREE 1 OPEN");
            comboBox2.Items.Insert(152, "98: SECRET BASE SPOT SHRUB");
            comboBox2.Items.Insert(153, "99: SECRET BASE SPOT SHRUB OPEN");
            comboBox2.Items.Insert(154, "9A: SECRET BASE SPOT BLUE CAVE");
            comboBox2.Items.Insert(155, "9B: SECRET BASE SPOT BLUE CAVE OPEN");
            comboBox2.Items.Insert(156, "9C: SECRET BASE SPOT TREE 2");
            comboBox2.Items.Insert(157, "9D: SECRET BASE SPOT TREE 2 OPEN");
            comboBox2.Items.Insert(158, "9E: UNUSED 9E");
            comboBox2.Items.Insert(159, "9F: UNUSED 9F");
            comboBox2.Items.Insert(160, "A0: BERRY TREE SOIL");
            comboBox2.Items.Insert(161, "A1: UNUSED A1");
            comboBox2.Items.Insert(162, "A2: UNUSED A2");
            comboBox2.Items.Insert(163, "A3: UNUSED A3");
            comboBox2.Items.Insert(164, "A4: UNUSED A4");
            comboBox2.Items.Insert(165, "A5: UNUSED A5");
            comboBox2.Items.Insert(166, "A6: UNUSED A6");
            comboBox2.Items.Insert(167, "A7: UNUSED A7");
            comboBox2.Items.Insert(168, "A8: UNUSED A8");
            comboBox2.Items.Insert(169, "A9: UNUSED A9");
            comboBox2.Items.Insert(170, "AA: UNUSED AA");
            comboBox2.Items.Insert(171, "AB: UNUSED AB");
            comboBox2.Items.Insert(172, "AC: UNUSED AC");
            comboBox2.Items.Insert(173, "AD: UNUSED AD");
            comboBox2.Items.Insert(174, "AE: UNUSED AE");
            comboBox2.Items.Insert(175, "AF: UNUSED AF");
            comboBox2.Items.Insert(176, "B0: SECRET BASE PC");
            comboBox2.Items.Insert(177, "B1: RECORD MIXING SECRET BASE PC");
            comboBox2.Items.Insert(178, "B2: SECRET BASE UNUSED");
            comboBox2.Items.Insert(179, "B3: BLOCK DECORATION");
            comboBox2.Items.Insert(180, "B4: SECRET BASE DECORATION");
            comboBox2.Items.Insert(181, "B5: SECRET BASE LARGE MAT EDGE");
            comboBox2.Items.Insert(182, "B6: UNUSED B6");
            comboBox2.Items.Insert(183, "B7: SECRET BASE NORTH WALL");
            comboBox2.Items.Insert(184, "B8: SECRET BASE BALLOON");
            comboBox2.Items.Insert(185, "B9: SECRET BASE IMPASSABLE");
            comboBox2.Items.Insert(186, "BA: SECRET BASE GLITTER MAT");
            comboBox2.Items.Insert(187, "BB: SECRET BASE JUMP MAT");
            comboBox2.Items.Insert(188, "BC: SECRET BASE SPIN MAT");
            comboBox2.Items.Insert(189, "BD: SECRET BASE MUSIC NOTE MAT");
            comboBox2.Items.Insert(190, "BE: SECRET BASE BREAKABLE DOOR");
            comboBox2.Items.Insert(191, "BF: SECRET BASE SAND ORNAMENT");
            comboBox2.Items.Insert(192, "C0: IMPASSABLE SOUTH AND NORTH");
            comboBox2.Items.Insert(193, "C1: IMPASSABLE WEST AND EAST");
            comboBox2.Items.Insert(194, "C2: SECRET BASE HOLE");
            comboBox2.Items.Insert(195, "C3: LARGE MAT CENTER");
            comboBox2.Items.Insert(196, "C4: SECRET BASE SHIELD OR TOY TV");
            comboBox2.Items.Insert(197, "C5: PLAYER ROOM PC ON");
            comboBox2.Items.Insert(198, "C6: UNUSED C6");
            comboBox2.Items.Insert(199, "C7: UNUSED C7");
            comboBox2.Items.Insert(200, "C8: UNUSED C8");
            comboBox2.Items.Insert(201, "C9: UNUSED C9");
            comboBox2.Items.Insert(202, "CA: UNUSED CA");
            comboBox2.Items.Insert(203, "CB: UNUSED CB");
            comboBox2.Items.Insert(204, "CC: UNUSED CC");
            comboBox2.Items.Insert(205, "CD: UNUSED CD");
            comboBox2.Items.Insert(206, "CE: UNUSED CE");
            comboBox2.Items.Insert(207, "CF: UNUSED CF");
            comboBox2.Items.Insert(208, "D0: MUDDY SLOPE");
            comboBox2.Items.Insert(209, "D1: BUMPY SLOPE");
            comboBox2.Items.Insert(210, "D2: CRACKED FLOOR");
            comboBox2.Items.Insert(211, "D3: ISOLATED VERTICAL RAIL");
            comboBox2.Items.Insert(212, "D4: ISOLATED HORIZONTAL RAIL");
            comboBox2.Items.Insert(213, "D5: VERTICAL RAIL");
            comboBox2.Items.Insert(214, "D6: HORIZONTAL RAIL");
            comboBox2.Items.Insert(215, "D7: UNUSED D7");
            comboBox2.Items.Insert(216, "D8: UNUSED D8");
            comboBox2.Items.Insert(217, "D9: UNUSED D9");
            comboBox2.Items.Insert(218, "DA: UNUSED DA");
            comboBox2.Items.Insert(219, "DB: UNUSED DB");
            comboBox2.Items.Insert(220, "DC: UNUSED DC");
            comboBox2.Items.Insert(221, "DD: UNUSED DD");
            comboBox2.Items.Insert(222, "DE: UNUSED DE");
            comboBox2.Items.Insert(223, "DF: UNUSED DF");
            comboBox2.Items.Insert(224, "E0: PICTURE BOOK SHELF");
            comboBox2.Items.Insert(225, "E1: BOOKSHELF");
            comboBox2.Items.Insert(226, "E2: POKEMON CENTER BOOKSHELF");
            comboBox2.Items.Insert(227, "E3: VASE");
            comboBox2.Items.Insert(228, "E4: TRASH CAN");
            comboBox2.Items.Insert(229, "E5: SHOP SHELF");
            comboBox2.Items.Insert(230, "E6: BLUEPRINT");
            comboBox2.Items.Insert(231, "E7: UNUSED E7");
            comboBox2.Items.Insert(232, "E8: UNUSED E8");
            comboBox2.Items.Insert(233, "E9: UNUSED E9");
            comboBox2.Items.Insert(234, "EA: UNUSED EA");
            comboBox2.Items.Insert(235, "EB: UNUSED EB");
            comboBox2.Items.Insert(236, "EC: UNUSED EC");
            comboBox2.Items.Insert(237, "ED: UNUSED ED");
            comboBox2.Items.Insert(238, "EE: UNUSED EE");
            comboBox2.Items.Insert(239, "EF: UNUSED EF");
            comboBox2.Items.Insert(240, "F0: UNUSED F0");
            comboBox2.Items.Insert(241, "F1: UNUSED F1");
            comboBox2.Items.Insert(242, "F2: UNUSED F2");
            comboBox2.Items.Insert(243, "F3: UNUSED F3");
            comboBox2.Items.Insert(244, "F4: UNUSED F4");
            comboBox2.Items.Insert(245, "F5: UNUSED F5");
            comboBox2.Items.Insert(246, "F6: UNUSED F6");
            comboBox2.Items.Insert(247, "F7: UNUSED F7");
            comboBox2.Items.Insert(248, "F8: UNUSED F8");
            comboBox2.Items.Insert(249, "F9: UNUSED F9");
            comboBox2.Items.Insert(250, "FA: UNUSED FA");
            comboBox2.Items.Insert(251, "FB: UNUSED FB");
            comboBox2.Items.Insert(252, "FC: UNUSED FC");
            comboBox2.Items.Insert(253, "FD: UNUSED FD");
            comboBox2.Items.Insert(254, "FE: UNUSED FE");
            comboBox2.Items.Insert(255, "FF: UNUSED FF");

            //ComboBox3
            comboBox3.Items.Clear();
            comboBox3.Items.Insert(0, "00");
            comboBox3.Items.Insert(1, "01");
            comboBox3.Items.Insert(2, "02");
            comboBox3.Items.Insert(3, "03");
            comboBox3.Items.Insert(4, "04");
            comboBox3.Items.Insert(5, "05");
            comboBox3.Items.Insert(6, "06");
            comboBox3.Items.Insert(7, "07");
            comboBox3.Items.Insert(8, "08");
            comboBox3.Items.Insert(9, "09");
            comboBox3.Items.Insert(10, "0A");
            comboBox3.Items.Insert(11, "0B");
            comboBox3.Items.Insert(12, "0C");
            comboBox3.Items.Insert(13, "0D");
            comboBox3.Items.Insert(14, "0E");
            comboBox3.Items.Insert(15, "0F");
            comboBox3.Items.Insert(16, "10: Block is covered by hero");
            comboBox3.Items.Insert(17, "11");
            comboBox3.Items.Insert(18, "12");
            comboBox3.Items.Insert(19, "13");
            comboBox3.Items.Insert(20, "14");
            comboBox3.Items.Insert(21, "15");
            comboBox3.Items.Insert(22, "16");
            comboBox3.Items.Insert(23, "17");
            comboBox3.Items.Insert(24, "18");
            comboBox3.Items.Insert(25, "19");
            comboBox3.Items.Insert(26, "1A");
            comboBox3.Items.Insert(27, "1B");
            comboBox3.Items.Insert(28, "1C");
            comboBox3.Items.Insert(29, "1D");
            comboBox3.Items.Insert(30, "1E");
            comboBox3.Items.Insert(31, "1F");
            comboBox3.Items.Insert(32, "20");
            comboBox3.Items.Insert(33, "21");
            comboBox3.Items.Insert(34, "22");
            comboBox3.Items.Insert(35, "23");
            comboBox3.Items.Insert(36, "24");
            comboBox3.Items.Insert(37, "25");
            comboBox3.Items.Insert(38, "26");
            comboBox3.Items.Insert(39, "27");
            comboBox3.Items.Insert(40, "28");
            comboBox3.Items.Insert(41, "29");
            comboBox3.Items.Insert(42, "2A");
            comboBox3.Items.Insert(43, "2B");
            comboBox3.Items.Insert(44, "2C");
            comboBox3.Items.Insert(45, "2D");
            comboBox3.Items.Insert(46, "2E");
            comboBox3.Items.Insert(47, "2F");
            comboBox3.Items.Insert(48, "30");
            comboBox3.Items.Insert(49, "31");
            comboBox3.Items.Insert(50, "32");
            comboBox3.Items.Insert(51, "33");
            comboBox3.Items.Insert(52, "34");
            comboBox3.Items.Insert(53, "35");
            comboBox3.Items.Insert(54, "36");
            comboBox3.Items.Insert(55, "37");
            comboBox3.Items.Insert(56, "38");
            comboBox3.Items.Insert(57, "39");
            comboBox3.Items.Insert(58, "3A");
            comboBox3.Items.Insert(59, "3B");
            comboBox3.Items.Insert(60, "3C");
            comboBox3.Items.Insert(61, "3D");
            comboBox3.Items.Insert(62, "3E");
            comboBox3.Items.Insert(63, "3F");
            comboBox3.Items.Insert(64, "40: Turns into border block");
            comboBox3.Items.Insert(65, "41");
            comboBox3.Items.Insert(66, "42");
            comboBox3.Items.Insert(67, "43");
            comboBox3.Items.Insert(68, "44");
            comboBox3.Items.Insert(69, "45");
            comboBox3.Items.Insert(70, "46");
            comboBox3.Items.Insert(71, "47");
            comboBox3.Items.Insert(72, "48");
            comboBox3.Items.Insert(73, "49");
            comboBox3.Items.Insert(74, "4A");
            comboBox3.Items.Insert(75, "4B");
            comboBox3.Items.Insert(76, "4C");
            comboBox3.Items.Insert(77, "4D");
            comboBox3.Items.Insert(78, "4E");
            comboBox3.Items.Insert(79, "4F");
            comboBox3.Items.Insert(80, "50");
            comboBox3.Items.Insert(81, "51");
            comboBox3.Items.Insert(82, "52");
            comboBox3.Items.Insert(83, "53");
            comboBox3.Items.Insert(84, "54");
            comboBox3.Items.Insert(85, "55");
            comboBox3.Items.Insert(86, "56");
            comboBox3.Items.Insert(87, "57");
            comboBox3.Items.Insert(88, "58");
            comboBox3.Items.Insert(89, "59");
            comboBox3.Items.Insert(90, "5A");
            comboBox3.Items.Insert(91, "5B");
            comboBox3.Items.Insert(92, "5C");
            comboBox3.Items.Insert(93, "5D");
            comboBox3.Items.Insert(94, "5E");
            comboBox3.Items.Insert(95, "5F");
            comboBox3.Items.Insert(96, "60");
            comboBox3.Items.Insert(97, "61");
            comboBox3.Items.Insert(98, "62");
            comboBox3.Items.Insert(99, "63");
            comboBox3.Items.Insert(100, "64");
            comboBox3.Items.Insert(101, "65");
            comboBox3.Items.Insert(102, "66");
            comboBox3.Items.Insert(103, "67");
            comboBox3.Items.Insert(104, "68");
            comboBox3.Items.Insert(105, "69");
            comboBox3.Items.Insert(106, "6A");
            comboBox3.Items.Insert(107, "6B");
            comboBox3.Items.Insert(108, "6C");
            comboBox3.Items.Insert(109, "6D");
            comboBox3.Items.Insert(110, "6E");
            comboBox3.Items.Insert(111, "6F");
            comboBox3.Items.Insert(112, "70");
            comboBox3.Items.Insert(113, "71");
            comboBox3.Items.Insert(114, "72");
            comboBox3.Items.Insert(115, "73");
            comboBox3.Items.Insert(116, "74");
            comboBox3.Items.Insert(117, "75");
            comboBox3.Items.Insert(118, "76");
            comboBox3.Items.Insert(119, "77");
            comboBox3.Items.Insert(120, "78");
            comboBox3.Items.Insert(121, "79");
            comboBox3.Items.Insert(122, "7A");
            comboBox3.Items.Insert(123, "7B");
            comboBox3.Items.Insert(124, "7C");
            comboBox3.Items.Insert(125, "7D");
            comboBox3.Items.Insert(126, "7E");
            comboBox3.Items.Insert(127, "7F");
            comboBox3.Items.Insert(128, "80");
            comboBox3.Items.Insert(129, "81");
            comboBox3.Items.Insert(130, "82");
            comboBox3.Items.Insert(131, "83");
            comboBox3.Items.Insert(132, "84");
            comboBox3.Items.Insert(133, "85");
            comboBox3.Items.Insert(134, "86");
            comboBox3.Items.Insert(135, "87");
            comboBox3.Items.Insert(136, "88");
            comboBox3.Items.Insert(137, "89");
            comboBox3.Items.Insert(138, "8A");
            comboBox3.Items.Insert(139, "8B");
            comboBox3.Items.Insert(140, "8C");
            comboBox3.Items.Insert(141, "8D");
            comboBox3.Items.Insert(142, "8E");
            comboBox3.Items.Insert(143, "8F");
            comboBox3.Items.Insert(144, "90");
            comboBox3.Items.Insert(145, "91");
            comboBox3.Items.Insert(146, "92");
            comboBox3.Items.Insert(147, "93");
            comboBox3.Items.Insert(148, "94");
            comboBox3.Items.Insert(149, "95");
            comboBox3.Items.Insert(150, "96");
            comboBox3.Items.Insert(151, "97");
            comboBox3.Items.Insert(152, "98");
            comboBox3.Items.Insert(153, "99");
            comboBox3.Items.Insert(154, "9A");
            comboBox3.Items.Insert(155, "9B");
            comboBox3.Items.Insert(156, "9C");
            comboBox3.Items.Insert(157, "9D");
            comboBox3.Items.Insert(158, "9E");
            comboBox3.Items.Insert(159, "9F");
            comboBox3.Items.Insert(160, "A0");
            comboBox3.Items.Insert(161, "A1");
            comboBox3.Items.Insert(162, "A2");
            comboBox3.Items.Insert(163, "A3");
            comboBox3.Items.Insert(164, "A4");
            comboBox3.Items.Insert(165, "A5");
            comboBox3.Items.Insert(166, "A6");
            comboBox3.Items.Insert(167, "A7");
            comboBox3.Items.Insert(168, "A8");
            comboBox3.Items.Insert(169, "A9");
            comboBox3.Items.Insert(170, "AA");
            comboBox3.Items.Insert(171, "AB");
            comboBox3.Items.Insert(172, "AC");
            comboBox3.Items.Insert(173, "AD");
            comboBox3.Items.Insert(174, "AE");
            comboBox3.Items.Insert(175, "AF");
            comboBox3.Items.Insert(176, "B0");
            comboBox3.Items.Insert(177, "B1");
            comboBox3.Items.Insert(178, "B2");
            comboBox3.Items.Insert(179, "B3");
            comboBox3.Items.Insert(180, "B4");
            comboBox3.Items.Insert(181, "B5");
            comboBox3.Items.Insert(182, "B6");
            comboBox3.Items.Insert(183, "B7");
            comboBox3.Items.Insert(184, "B8");
            comboBox3.Items.Insert(185, "B9");
            comboBox3.Items.Insert(186, "BA");
            comboBox3.Items.Insert(187, "BB");
            comboBox3.Items.Insert(188, "BC");
            comboBox3.Items.Insert(189, "BD");
            comboBox3.Items.Insert(190, "BE");
            comboBox3.Items.Insert(191, "BF");
            comboBox3.Items.Insert(192, "C0");
            comboBox3.Items.Insert(193, "C1");
            comboBox3.Items.Insert(194, "C2");
            comboBox3.Items.Insert(195, "C3");
            comboBox3.Items.Insert(196, "C4");
            comboBox3.Items.Insert(197, "C5");
            comboBox3.Items.Insert(198, "C6");
            comboBox3.Items.Insert(199, "C7");
            comboBox3.Items.Insert(200, "C8");
            comboBox3.Items.Insert(201, "C9");
            comboBox3.Items.Insert(202, "CA");
            comboBox3.Items.Insert(203, "CB");
            comboBox3.Items.Insert(204, "CC");
            comboBox3.Items.Insert(205, "CD");
            comboBox3.Items.Insert(206, "CE");
            comboBox3.Items.Insert(207, "CF");
            comboBox3.Items.Insert(208, "D0");
            comboBox3.Items.Insert(209, "D1");
            comboBox3.Items.Insert(210, "D2");
            comboBox3.Items.Insert(211, "D3");
            comboBox3.Items.Insert(212, "D4");
            comboBox3.Items.Insert(213, "D5");
            comboBox3.Items.Insert(214, "D6");
            comboBox3.Items.Insert(215, "D7");
            comboBox3.Items.Insert(216, "D8");
            comboBox3.Items.Insert(217, "D9");
            comboBox3.Items.Insert(218, "DA");
            comboBox3.Items.Insert(219, "DB");
            comboBox3.Items.Insert(220, "DC");
            comboBox3.Items.Insert(221, "DD");
            comboBox3.Items.Insert(222, "DE");
            comboBox3.Items.Insert(223, "DF");
            comboBox3.Items.Insert(224, "E0");
            comboBox3.Items.Insert(225, "E1");
            comboBox3.Items.Insert(226, "E2");
            comboBox3.Items.Insert(227, "E3");
            comboBox3.Items.Insert(228, "E4");
            comboBox3.Items.Insert(229, "E5");
            comboBox3.Items.Insert(230, "E6");
            comboBox3.Items.Insert(231, "E7");
            comboBox3.Items.Insert(232, "E8");
            comboBox3.Items.Insert(233, "E9");
            comboBox3.Items.Insert(234, "EA");
            comboBox3.Items.Insert(235, "EB");
            comboBox3.Items.Insert(236, "EC");
            comboBox3.Items.Insert(237, "ED");
            comboBox3.Items.Insert(238, "EE");
            comboBox3.Items.Insert(239, "EF");
            comboBox3.Items.Insert(240, "F0");
            comboBox3.Items.Insert(241, "F1");
            comboBox3.Items.Insert(242, "F2");
            comboBox3.Items.Insert(243, "F3");
            comboBox3.Items.Insert(244, "F4");
            comboBox3.Items.Insert(245, "F5");
            comboBox3.Items.Insert(246, "F6");
            comboBox3.Items.Insert(247, "F7");
            comboBox3.Items.Insert(248, "F8");
            comboBox3.Items.Insert(249, "F9");
            comboBox3.Items.Insert(250, "FA");
            comboBox3.Items.Insert(251, "FB");
            comboBox3.Items.Insert(252, "FC");
            comboBox3.Items.Insert(253, "FD");
            comboBox3.Items.Insert(254, "FE");
            comboBox3.Items.Insert(255, "FF");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            setAttributeBytes();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            setAttributeBytes();
        }

        private void setAttributeBytes()
        {
            PictureBox blockSel = this.Controls.OfType<PictureBox>().FirstOrDefault(x => x.Name == "blockSelected");
            if (blockSel != null)
            {
                var a = comboBox2.SelectedIndex.ToString("X");
                var b = comboBox3.SelectedIndex.ToString("X");
                if (a.Length == 1)
                {
                    a = "0" + a;
                }
                if (b.Length == 1)
                {
                    b = "0" + b;
                }
                var byteToLoad = a + b;
                var block = blockSelected;
                var blockFirstString = block;
                //MessageBox.Show((Int32.Parse(blockSelected)).ToString());
                BinaryReader br = new BinaryReader(File.OpenRead(binFile));
                var firstFileLength = Int32.Parse((br.BaseStream.Length / 16).ToString());
                br.Close();
                bool usingTileset2 = false;
                if (Int32.Parse(block) >= firstFileLength)
                {
                    block = (Int32.Parse(block) - firstFileLength).ToString();
                    insertBytes((Int32.Parse(block) * 2).ToString("X"), byteToLoad, tl2BinAttributeFile);
                    usingTileset2 = true;
                } else
                {
                    insertBytes((Int32.Parse(block) * 2).ToString("X"), byteToLoad, binAttributeFile);
                    usingTileset2 = false;
                }
                //reloadFileAttribute();

                int posProView = ((Int32.Parse(block) / 8) * 59) + ((Int32.Parse(block) % 8) * 6) + 10;
                int posString = Int32.Parse(block) * 4;

                if (usingTileset2 == true)
                {
                    string valueProViewPre1 = tl2UltraStringAttribute.Substring(0, posProView);
                    string valueProViewPre2 = tl2UltraStringAttribute.Substring(posProView + 6);
                    tl2UltraStringAttribute = valueProViewPre1 + a + " " + b + " " + valueProViewPre2;
                    richTextBox4.Text = tl2UltraStringAttribute;
                } else
                {
                    string valueProViewPre1 = ultraStringAttribute.Substring(0, posProView);
                    string valueProViewPre2 = ultraStringAttribute.Substring(posProView + 6);
                    ultraStringAttribute = valueProViewPre1 + a + " " + b + " " + valueProViewPre2;
                    richTextBox2.Text = ultraStringAttribute;
                }

                block = blockFirstString;

                posProView = ((Int32.Parse(block) / 8) * 59) + ((Int32.Parse(block) % 8) * 6) + 10;
                posString = Int32.Parse(block) * 4;

                string valueStringPre1 = completeValueAttribute.Substring(0, posString);
                string valueStringPre2 = completeValueAttribute.Substring(posString + 4);
                completeValueAttribute = valueStringPre1 + byteToLoad + valueStringPre2;
                /*string valueProViewPre1 = ultraStringAttribute.Substring(0, posProView);
                string valueProViewPre2 = ultraStringAttribute.Substring(posProView + 6);
                ultraStringAttribute = valueProViewPre1 + a + " " + b + " " + valueProViewPre2;
                richTextBox2.Text = ultraStringAttribute;*/


                /*PRUEBA
                int posProView = Int32.Parse(bloque) * 59 + Int32.Parse(txtDef) * 6 + 10;
                int posString = Int32.Parse(bloque) * 32 + Int32.Parse(txtDef) * 4;

                if (usingTileset2 == true)
                {
                    string valueProViewPre1 = tl2UltraString.Substring(0, posProView);
                    string valueProViewPre2 = tl2UltraString.Substring(posProView + 6);
                    tl2UltraString = valueProViewPre1 + stringTile1 + " " + selectedPalette.ToString("X") + stringTile2 + " " + valueProViewPre2;
                    richTextBox3.Text = tl2UltraString;
                } else { 
                    string valueProViewPre1 = ultraString.Substring(0, posProView);
                    string valueProViewPre2 = ultraString.Substring(posProView + 6);
                    ultraString = valueProViewPre1 + stringTile1 + " " + selectedPalette.ToString("X") + stringTile2 + " " + valueProViewPre2;
                    richTextBox1.Text = ultraString;
                }
                bloque = bloqueNumeroString;
                posProView = Int32.Parse(bloque) * 59 + Int32.Parse(txtDef) * 6 + 10;
                posString = Int32.Parse(bloque) * 32 + Int32.Parse(txtDef) * 4;

                string valueStringPre1 = completeValue.Substring(0, posString);
                string valueStringPre2 = completeValue.Substring(posString + 4);
                completeValue = valueStringPre1 + stringTile1 + selectedPalette.ToString("X") + stringTile2 + valueStringPre2;*/

            }
        }

        private void hideAll()
        {
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            button4.Visible = false;
            tabControl1.Visible = false;
        }

        private void showAll()
        {
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
            comboBox4.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            checkBox1.Visible = true;
            checkBox2.Visible = true;
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            button4.Visible = true;
            tabControl1.Visible = true;
        }

        private void debugger2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var img = tileset;
            var img2 = tl2Tileset;
            Bitmap rr = new Bitmap(tileset.Width, tileset.Height + tl2Tileset.Height);
            var graphics = Graphics.FromImage(rr);
            graphics.DrawImage(img, new Rectangle(0, 0, tileset.Width, tileset.Height), new Rectangle(0, 0, tileset.Width, tileset.Height), GraphicsUnit.Pixel);
            graphics.DrawImage(img2, new Rectangle(0, tileset.Height, tl2Tileset.Width, tl2Tileset.Height), new Rectangle(0, 0, tl2Tileset.Width, tl2Tileset.Height), GraphicsUnit.Pixel);
            graphics.Dispose();
            tileset = rr;

            reloadFile();
            reloadFileAttribute();
            setAttributeNames();
            loadBlockTiles();
            setColorTransparency(0, 0, false);
            changePalette(0);
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            checkBox1.Visible = true;
            checkBox2.Visible = true;
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            vistaProfesionalToolStripMenuItem.Enabled = true;
            PictureBox callSetBlock = panel2.Controls.OfType<PictureBox>().FirstOrDefault(x => x.Name == "blockTile0");
            if (callSetBlock != null)
            {
                setTileBlock_MouseClick(callSetBlock, null);
            }
            PictureBox callSetTile = panel1.Controls.OfType<PictureBox>().FirstOrDefault(x => x.Name == "block0");
            if (callSetTile != null)
            {
                setBlock_MouseClick(callSetTile, null);
            }
            //this.Controls.Add(picture);
        }

        private void modoDeVentanaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void vaciarTilesetPrincipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(bytesToLoad.ToString());
            hideAll();
            BinaryWriter bw = new BinaryWriter(File.OpenWrite(binFile));

            bw.BaseStream.Position = 0;
            byte[] buffer = new byte[bytesToLoad];
            Array.Reverse(buffer); //Permutar
            bw.Write(buffer);

            bw.Dispose();

            BinaryWriter bw2 = new BinaryWriter(File.OpenWrite(binAttributeFile));

            bw2.BaseStream.Position = 0;
            byte[] buffer2 = new byte[bytesToLoadAttribute];
            Array.Reverse(buffer2); //Permutar
            bw2.Write(buffer2);

            bw2.Dispose();

            setPalette(0);
            comboBox1.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            if (tileset != null)
            {
                createBlocks();

                blockPicked = 0;
                label1.Text = "Tile: " + blockPicked.ToString() + "00";
                createNewBlock(0);
                resetFlipSystem();
            }
            reloadFile();
            reloadFileAttribute();
            loadBlockTiles();
            setColorTransparency(0, 0, false);
            changePalette(0);
            vistaProfesionalToolStripMenuItem.Enabled = true;
            importarTilesetSecundarioToolStripMenuItem.Enabled = true;
            PictureBox callSetBlock = panel2.Controls.OfType<PictureBox>().FirstOrDefault(x => x.Name == "blockTile0");
            if (callSetBlock != null)
            {
                setTileBlock_MouseClick(callSetBlock, null);
            }
            PictureBox callSetTile = panel1.Controls.OfType<PictureBox>().FirstOrDefault(x => x.Name == "block0");
            if (callSetTile != null)
            {
                setBlock_MouseClick(callSetTile, null);
            }
            showAll();
        }

        private void vaciarTilesetSecundarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hideAll();
            BinaryWriter bw = new BinaryWriter(File.OpenWrite(tl2BinFile));

            bw.BaseStream.Position = 0;
            byte[] buffer = new byte[tl2BytesToLoad];
            Array.Reverse(buffer); //Permutar
            bw.Write(buffer);

            bw.Dispose();

            BinaryWriter bw2 = new BinaryWriter(File.OpenWrite(tl2BinAttributeFile));

            bw2.BaseStream.Position = 0;
            byte[] buffer2 = new byte[tl2BytesToLoadAttribute];
            Array.Reverse(buffer2); //Permutar
            bw2.Write(buffer2);

            bw2.Dispose();
            
            setPalette(0);
            comboBox1.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            if (tileset != null)
            {
                tl2CreateBlocks();
                blockPicked = 0;
                label1.Text = "Tile: " + blockPicked.ToString() + "00";
                createNewBlock(0);
                resetFlipSystem();
            }
            reloadFile();
            reloadFileAttribute();
            loadBlockTiles();
            setColorTransparency(0, 0, false);
            changePalette(0);
            vistaProfesionalToolStripMenuItem.Enabled = true;
            PictureBox callSetBlock = panel2.Controls.OfType<PictureBox>().FirstOrDefault(x => x.Name == "blockTile0");
            if (callSetBlock != null)
            {
                setTileBlock_MouseClick(callSetBlock, null);
            }
            PictureBox callSetTile = panel1.Controls.OfType<PictureBox>().FirstOrDefault(x => x.Name == "block0");
            if (callSetTile != null)
            {
                setBlock_MouseClick(callSetTile, null);
            }
            showAll();
        }

        private void folderManager()
        {
            int maxFolderValue = 0;
            int securityCopiesAmount = 10;
            string newFolderValue = "";
            bool zeroEmpty = false;
            if (Directory.Exists(Application.StartupPath + "\\seccp\\00"))
            {
                if (Directory.GetFiles(Application.StartupPath + "\\seccp\\00", "*.*" ,SearchOption.AllDirectories).Length == 0)
                {
                    zeroEmpty = true;
                } else
                {
                    zeroEmpty = false;
                }
            }

            if (zeroEmpty == false)
            {

                if (Directory.Exists(Application.StartupPath + "\\seccp\\") == false)
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\seccp");
                }

                for (int i = 0; i < (securityCopiesAmount - 1); i++)
                {
                    string stringFolderValue = maxFolderValue.ToString();

                    if (stringFolderValue.Length == 1)
                    {
                        stringFolderValue = "0" + stringFolderValue;
                    }

                    if (Directory.Exists(Application.StartupPath + "\\seccp\\" + stringFolderValue))
                    {
                        //MessageBox.Show("\\seccp\\" + stringFolderValue + " existe");
                        maxFolderValue++;
                    }
                    else
                    {
                        //MessageBox.Show("\\seccp\\" + stringFolderValue + " NO existe");
                    }
                }

                if (maxFolderValue == (securityCopiesAmount - 1))
                {
                    string stringFolderValue = maxFolderValue.ToString();
                    if (stringFolderValue.Length == 1)
                    {
                        stringFolderValue = "0" + stringFolderValue;
                    }
                    if (Directory.Exists(Application.StartupPath + "\\seccp\\" + stringFolderValue))
                    {
                        Directory.Delete(Application.StartupPath + "\\seccp\\" + stringFolderValue, true);
                    }

                }

                if (maxFolderValue.ToString().Length == 1)
                {
                    newFolderValue = "0" + maxFolderValue;
                }
                else
                {
                    newFolderValue = maxFolderValue.ToString();
                }

                Directory.CreateDirectory(Application.StartupPath + "\\seccp\\" + newFolderValue);

                for (int i = maxFolderValue; i > 0; i--)
                {
                    string stringFolderValue = i.ToString();
                    string stringFolderValueMinOne = (i - 1).ToString();
                    //MessageBox.Show(stringFolderValue);

                    if (stringFolderValue.Length == 1)
                    {
                        stringFolderValue = "0" + stringFolderValue;
                    }
                    if (stringFolderValueMinOne.Length == 1)
                    {
                        stringFolderValueMinOne = "0" + stringFolderValueMinOne;
                    }
                    //MessageBox.Show(stringFolderValue);
                    if (Directory.Exists(Application.StartupPath + "\\seccp\\" + stringFolderValue))
                    {
                        Directory.Delete(Application.StartupPath + "\\seccp\\" + stringFolderValue, true);
                    }
                    System.IO.Directory.Move((Application.StartupPath + "\\seccp\\" + stringFolderValueMinOne), (Application.StartupPath + "\\seccp\\" + stringFolderValue));
                }
                if (Directory.Exists(Application.StartupPath + "\\seccp\\00") == false)
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\seccp\\00");
                }
            }
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version: 1.0\n\nThis tool was created by InmortalKaktus and Laquin, and translated by Lunos.\n\nIt is strongly suggested not to use the vertical scrollbar because it can hurt the tool's performance.",
            "Information",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
 