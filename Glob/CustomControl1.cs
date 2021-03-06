﻿using System;
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

namespace Glob
{
    public class CustomControl1 : Control
    {
        static CustomControl1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl1), new FrameworkPropertyMetadata(typeof(CustomControl1)));
        }
    }
    public static class Global
    {
        public static readonly string datetimeDateonlyMask = "00/00/0000";
        public static readonly string datetimeDateonlyFormat = "dd/MM/yyyy";
        public static readonly string datetimeTimeonlyFormat = "HH:mm";
        public static readonly string datetimeTimeonlyMask = "00:00";
        public static readonly string datetimeTimeonlyMaskedDisplay = "__:__";
        public static readonly string numberMask = "000";

        public static string DATETIME_TIMEONLY_MASK
        {
            get
            {
                return datetimeTimeonlyMask;

            }
        }
        public static string DATETIME_TIMEONLY_FORMAT
        {
            get
            {
                return datetimeTimeonlyFormat;

            }
        }
        public static string DATETIME_DATEONLY_MASK
        {
            get
            {
                return datetimeDateonlyMask;
            }
        }
        public static string NUMBER_MASK
        {
            get { return numberMask; }
        }
    }
}
