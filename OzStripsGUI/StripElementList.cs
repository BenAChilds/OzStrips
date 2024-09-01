﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using vatsys;

namespace MaxRumsey.OzStripsPlugin.Gui;

/// <summary>
/// Represents a list of strip elements.
/// </summary>
[Serializable]
[XmlRoot("StripElementList")]
public class StripElementList
{
    /// <summary>
    /// Gets or sets a list of strip elements.
    /// </summary>
    [XmlElement("StripElement")]
    public List<StripElement> List { get; set; } = new List<StripElement>();

    internal static StripElementList? Deserialize(string path)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(StripElementList));
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            StripElementList element;

            element = (StripElementList)serializer.Deserialize(fs);

            return element;
        }
        catch (Exception ex) {
            Errors.Add(ex, "OzStrips");
        }

        return null;
    }
}
