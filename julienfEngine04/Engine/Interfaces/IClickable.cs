using System;
using System.Collections.Generic;
using System.Text;

namespace julienfEngine1
{
    interface IClickable
    {
        public void OnSelect();

        public void OnDeselect();

        public void OnClick();
    }
}
