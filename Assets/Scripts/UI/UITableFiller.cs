using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;

/** 
 * Class to Fill UI tables with Variable-Names, 0s and 1s. 
 * The Filler uses multiple ways to fill tables.
 */
public class UITableFiller : MonoBehaviour
{
    /** 
     * Adds a TextMeshPro Element to the given panel with the given Text and the given Color.
     * @param   panel   A UI panel that is layed out as a grid. The newly created Gameobject will be added to it.     
     * @param   text    A string that will be written on the GameObject, using TextMeshPro
     * @param   color   A Color in which the Text on the Gameobject will be presented.
     * @return          Reference to the newly created TextMeshPro GameObject.
     */
    public static GameObject AddTextToPanel(GameObject panel, string text, Color color)
    {
        GameObject temp = new GameObject(text, typeof(TextMeshProUGUI));
        temp.transform.SetParent(panel.transform, false);
        TextMeshProUGUI tempText = temp.GetComponent<TextMeshProUGUI>();
        tempText.alignment = TextAlignmentOptions.Center;
        tempText.color = color;
        tempText.text = text;
        tempText.font = tempText.font.fallbackFontAssetTable[0];
        tempText.fontMaterial = tempText.font.material;
        tempText.fontSizeMin = 10;
        tempText.fontSizeMax = 32;
        tempText.enableAutoSizing = true;
        return temp;
    }
    /**
     * Fills a given panel with a list of booleanTerm-objects.
     * If the List is empty, the panel is empty.
     * @param   panel           The panel in which to insert Textobjects with given boolean terms.
     * @param   booleanTerms    A list of boolean terms which then will be converted to text and added to a Textobject.
     */
    public static void FillTable(GameObject panel, List<KVHandler.BooleanTerm> booleanTerms)
    {
        panel.GetComponent<GridLayoutGroup>().constraintCount = 1;
        panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(250, 50);
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(KVHandler.BooleanTerm booleanTerm in booleanTerms)
        {
            GameObject temp = AddTextToPanel(panel, booleanTerm.expression+" v", Color.black);
            UIClickable tempClickable = temp.AddComponent<UIClickable>();
            tempClickable._booleanTerm = booleanTerm;
        }
    }

    /**
     * Fills a given panel with textobjects to form a truthtable. If the scene has multiple panels which share vertical space, the panelsize will be adjusted.
     * All elements in the panels will then be adjusted to fit the calculated panelsize.
     * The Truthtable consists of a list of resultnumbers.
     * If the result in a row is equal to the result of the expectedResults-List the Text in this row will be green. Else it will be red.
     * To show the expected Results in a panel, results must be the same as expectedResults.
     * If the List is empty, the panel is empty.
     * @param   panel           The panel in which to insert Textobjects with given boolean terms.
     * @param   results         List of results to be presented on the truthtable.
     * @param   variableCount   Number of inputs of the truthtable.
     * @param   expectedResults List of correct results. Used to compare given results with correct results.
     */
    public static void FillTable(GameObject panel, int[] results, int variableCount, int[] expectedResults, int panelcount = 1)
    {
        // Setting the Panelheight and width, aswell as the number of elements in the panel (x elements horizontally and y elements vertically).
        float width = 250;
        float height = 500 / panelcount;
        panel.GetComponent<GridLayoutGroup>().constraintCount = variableCount + 1;
        float x = width / (variableCount + 1);
        float y = height / (Mathf.Pow(2, variableCount) + 1);
        panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(x, y);

        // Remove all Objects of the given panel.
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }

        // Add the first row, consisting of the variable-names and one "Out"-field.
        for (int i = variableCount - 1; i >= 0; i--)
        {
            AddTextToPanel(panel, ((Charge)i + 1).ToString(), Color.black);
        }
        AddTextToPanel(panel, "Out", Color.black);


        // Fill the rest of the Panel by iterating through the Elements row by row.
        for (int i = 0; i < Math.Pow(2, variableCount); i++)
        {
            // Set the Color of the text. If the result equals the expected result of the given row, it will be green. Otherwise it will be red.
            Color colorTemp = Color.red;
            if (results != null)
            {
                colorTemp = results[i] == expectedResults[i] ? Color.green : Color.red;
            }

            // Add elements for each element of the row.
            for (int j = variableCount-1; j >= 0; j--)
            {
                // Decoding the current result for the j-element. The j-bit on int i tells the status of the element.
                // Variable A is the first (LSB) bit of int i. B is the second bit of int i and so on.
                // If int i is 10, the Code on 4 Variables would be 1010. A = 0, B = 1, C = 0, D = 1.
                string textTemp;
                if ((i & (1 << j)) != 0)
                {
                    textTemp = "1";
                }
                else
                {
                    textTemp = "0";
                }
                AddTextToPanel(panel, textTemp, colorTemp);
            }

            // Add the output of the current row, given by the results-list. This is the int on results[i].
            if (results != null)
            {
                AddTextToPanel(panel, results[i].ToString(), colorTemp);
            }
            else
            {
                AddTextToPanel(panel, "", colorTemp);
            }
        }
    }

    /**
     * Filling a table to a truthtable with more than one Output.
     * @param   panel           The Panel Gameobject to fill with elements.
     * @param   results         A list of ints which hold the results to be displayed.
     * @param   outputCount     Number of outputs.
     * @param   variableCount   Number of variables.
     * @param   expectedResults The correct results for the list. This is used to color rows of the truthtable.
     * @param   setResults      Lists, which output on which row has a valid charge. If the charge is invalid, the output on this row will be -1.
     * @param   width           width of the canvas. Default is 800.
     * @param   height          height of the canvas. Default is 400.
     */ 
    public static void FillTable(GameObject panel, int[] results, int outputCount, int variableCount, int[] expectedResults, bool[][] setResults = null, int width = 800, int height = 400)
    {
        // Set the dimensions of the panel aswell as the elementsize (x and y).
        panel.GetComponent<GridLayoutGroup>().constraintCount = variableCount + outputCount;
        float x = width / (variableCount + outputCount);
        float y = height / (Mathf.Pow(2, variableCount) + 1);
        panel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(x, y);

        // Empty the given panel.
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }

        // Add a first row consisting of variables and outputs.
        for (int i = variableCount - 1; i >= 0; i--)
        {
            AddTextToPanel(panel, ((Charge)i + 1).ToString(), Color.black);
        }
        for (int i = 0; i < outputCount; i++)
        {
            AddTextToPanel(panel, "Out "+i, Color.black);
        }


        // Fill the rest of the table row by row.
        for (int i = 0; i < Math.Pow(2, variableCount); i++)
        {
            // Calculate the color of the current row.
            Color colorTemp = Color.red;
            // If there are comparable results and all outputs have valid values, check if the results of this row are equal to the expected results.
            // If the results are equal, the row will be green. In all other cases, the row will be red.
            if (results != null && (setResults == null || !setResults[i].Contains(false)))
            {
                colorTemp = results[i] == expectedResults[i] ? Color.green : Color.red;
            }
            // Create text-elements for each input variable for the given i.
            for (int j = variableCount - 1; j >= 0; j--)
            {                 
                // Decoding the current result for the j-element. The j-bit on int i tells the status of the element.
                // Variable A is the first (LSB) bit of int i. B is the second bit of int i and so on.
                // If int i is 10, the Code on 4 Variables would be 1010. A = 0, B = 1, C = 0, D = 1.
                string textTemp;
                if ((i & (1 << j)) != 0)
                {
                    textTemp = "1";
                }
                else
                {
                    textTemp = "0";
                }
                AddTextToPanel(panel, textTemp, colorTemp);
            }

            // If there are comparable results given, calculate the setting of each output.
            if (results != null)
            {
                // Decodes the output-value by bitshifting the int on results[i].
                // The first outputvariable is coded into the first bit of the int on results[i].
                int res = 0;
                for (int j = 0; j < outputCount; j++)
                {
                    res |= ((1 << j) & results[i]);
                    res >>= j;

                    // Check, if there is a setResults-list, if the output has been set correctly. If not, change add -1. 
                    // If it is set correctly, set it to the decoded value.
                    if (setResults != null)
                    {
                        if (!setResults[i][j])
                        {
                            AddTextToPanel(panel, (-1).ToString(), colorTemp);
                        }
                        else
                        {
                            AddTextToPanel(panel, res.ToString(), colorTemp);
                        }
                    }else
                    {
                        AddTextToPanel(panel, res.ToString(), colorTemp);
                    }
                    // Reset res to 0. Could also be done before the decoding I guess?
                    res = 0;
                }
            }
            // If there is no results given, add a blank text-element to the corresponding field.
            else
            {
                AddTextToPanel(panel, "", colorTemp);
            }
        }
    }
}
