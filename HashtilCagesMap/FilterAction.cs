using HashtilCagesMap.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Windows.Data;
using System.Windows.Interactivity;


namespace HashtilCagesMap
{
    public class FilterAction : TargetedTriggerAction<ComboBoxAdv>
    {
        protected override void Invoke(object parameter)
        {
            CollectionView items = (CollectionView)CollectionViewSource.GetDefaultView(Target.ItemsSource);
            items.Filter = ((o) =>
            {
                if (String.IsNullOrEmpty(Target.Text))
                    return true;
                else
                {
                    if ((o as TotalCageCx).CxName.Contains(Target.Text))
                        return true;
                    else
                        return false;
                }
            });
            items.Refresh();
        }
    }
}