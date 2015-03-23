//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Windows.UI.Xaml;
//using Windows.UI;
//using Windows.UI.Xaml.Controls;

//namespace VolleyballStats.Controls
//{
//    public class DragDropListView : Windows.UI.Xaml.Controls.ListView
//    {
//        private int _dropIndex;
//        private bool _hasHintedState;

//        protected override void OnDragEnter(DragEventArgs e)
//        {
//            base.OnDragEnter(e);

//            DropAt = 0;
//            _hasHintedState = false;
//        }

//        protected override void OnDragOver(DragEventArgs e)
//        {
//            base.OnDragOver(e);
 
//            var position = e.GetPosition(null);
 
//            //Get the list of items under the current position
//            var hitListViewItems =VisualTreeHelper.FindElementsInHostCoordinates(position, this)
//                    .OfType()
//                    .ToArray();
 
//            if (!hitListViewItems.Any())
//            {
//                if (!_hasHintedState)
//                {
//                    //We're over the list but not any particular items so add to the end
//                    DropAt = Items.Count;
//                }
 
//                return;
//            }
 
//            //Get all of the list items
//            var itemContainers = Items.Select(x => ItemContainerGenerator.ContainerFromItem(x))
//                    .OfType()
//                    .ToArray();
 
//            var dropItem = itemContainers.FirstOrDefault(hitListViewItems.Contains);
//            if (dropItem == null)
//            {
//                //Don't change anything - we're not hitting any list item because one or more are already
//                //in a hinted state
//                return;
//            }
 
//            _dropIndex = ItemContainerGenerator.IndexFromContainer(dropItem);
//            DropAt = _dropIndex;
             
//            _hasHintedState = true;
 
//            //Updated - we need to check for an offset if the list has been scrolled
//            var offset = GetIndexOfFirstSelector(this);
 
//            //Update the hinted state for all the items
//            for (var i=0; i < itemContainers.Count(); i++)
//            {
//                string hint = "NoReorderHint";
                 
//                if (i = _dropIndex)
//                    hint = "BottomReorderHint";
 
//                VisualStateManager.GoToState(itemContainers[i], hint, true);
//            }
 
//        }

//        protected override void OnDragLeave(DragEventArgs e)
//        {
//            base.OnDragLeave(e);
 
//            var list = Items.Select(x => ItemContainerGenerator.ContainerFromItem(x)); //.OfType()
//            //When leaving the Drag operation, remove any order hint states
//            foreach (var item in list)
//                    VisualStateManager.GoToState(item, "NoReorderHint", true);
//        }

//        private int GetIndexOfFirstSelector(ListViewBase listViewBase)
//        {
//            //Get the index of the first Selector item as this will provide an offset for setting the hinted state
//            var offset = 0;

//            var items = listViewBase.Items.Where(x => x != null).Select(x => listViewBase.ItemContainerGenerator.ContainerFromItem(x))
//                                    .ToArray();

//            for (var i = 0; i < items.Count(); i++)
//            {
//                //if (!(items[i] is SelectorItem))
//                //    continue;

//                offset = i;
//                break;
//            }

//            return offset;
//        }

//        /// <summary>
//        /// A property indicating the position in the list where the item(s) should be dropped
//        /// </summary>
//        public int DropAt { get; set; }

//    }

//}
