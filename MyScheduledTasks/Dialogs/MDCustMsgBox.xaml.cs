﻿// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

// Inspired by https://stackoverflow.com/a/60302166

namespace MyScheduledTasks.Dialogs;

public partial class MDCustMsgBox : Window
{
    #region Public Property
    public static CustResultType CustResult { get; set; }
    #endregion

    /// <summary>
    /// Custom message box for MDIX
    /// </summary>
    /// <param name="Message">Text of the message</param>
    /// <param name="Title">Text that goes in the title bar</param>
    /// <param name="Buttons">OK, OKCancel, YesNoCancel or YesNo</param>
    /// <param name="HideClose">True to hide close button</param>
    /// <param name="OnTop">True to make window topmost</param>
    /// <param name="MsgBoxOwner">Owner of the window</param>
    /// <param name="IsError">True will set accent color to red</param>
    public MDCustMsgBox(string Message,
                        string Title,
                        ButtonType Buttons,
                        bool HideClose = false,
                        bool OnTop = true,
                        Window MsgBoxOwner = null,
                        bool IsError = false)
    {
        InitializeComponent();

        #region Topmost
        if (OnTop)
        {
            Topmost = true;
        }
        #endregion

        #region Message text
        txtMessage.Text = Message;
        #endregion Message text

        #region Message box title
        if (string.IsNullOrEmpty(Title))
        {
            txtTitle.Text = Application.Current.MainWindow.Title;
        }
        else
        {
            txtTitle.Text = Title;
        }
        #endregion Message box title

        #region Button visibility
        switch (Buttons)
        {
            case ButtonType.Ok:
                btnCancel.Visibility = Visibility.Collapsed;
                btnYes.Visibility = Visibility.Collapsed;
                btnNo.Visibility = Visibility.Collapsed;
                _ = btnOk.Focus();
                break;

            case ButtonType.OkCancel:
                btnYes.Visibility = Visibility.Collapsed;
                btnNo.Visibility = Visibility.Collapsed;
                _ = btnOk.Focus();
                break;

            case ButtonType.YesNo:
                btnOk.Visibility = Visibility.Collapsed;
                btnCancel.Visibility = Visibility.Collapsed;
                _ = btnYes.Focus();
                break;

            case ButtonType.YesNoCancel:
                btnOk.Visibility = Visibility.Collapsed;
                _ = btnYes.Focus();
                break;
        }
        if (HideClose)
        {
            btnClose.Visibility = Visibility.Collapsed;
        }
        #endregion Button visibility

        #region Window position
        if (MsgBoxOwner != null)
        {
            Owner = MsgBoxOwner;
            if (Owner.IsVisible)
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
        }
        else
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        #endregion Window position

        #region Error message
        if (IsError)
        {
            BorderBrush = System.Windows.Media.Brushes.OrangeRed;
            BorderThickness = new Thickness(2);
            cardHeader.Background = BorderBrush;
            cardHeader.FontWeight = FontWeights.Bold;
        }
        #endregion Error message

    }

    #region Button and mouse events
    private void Btn_Click_Ok(object sender, RoutedEventArgs e)
    {
        Close();
        CustResult = CustResultType.Ok;
    }

    private void Btn_Click_Yes(object sender, RoutedEventArgs e)
    {
        Close();
        CustResult = CustResultType.Yes;
    }
    private void Btn_Click_No(object sender, RoutedEventArgs e)
    {
        Close();
        CustResult = CustResultType.No;
    }

    private void Btn_Click_Cancel(object sender, RoutedEventArgs e)
    {
        Close();
        CustResult = CustResultType.Cancel;
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }
    #endregion Button and mouse events
}

#region Button type enumeration
public enum ButtonType
{
    OkCancel,
    YesNo,
    YesNoCancel,
    Ok,
}
#endregion Button type enumeration

#region Result type enumeration
public enum CustResultType
{
    Ok,
    Yes,
    No,
    Cancel
}
#endregion Result type enumeration
