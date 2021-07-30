using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaitoMajima
{
    public class PageCycles : MonoBehaviour
    {
        [SerializeField] private List<GameObject> pages;
        private int pageIndex;

        private void Start()
        {
            OpenPage();
        }

        private void ClosePage()
        {
            var page = pages[pageIndex];
            page.SetActive(false);
        }

        private void OpenPage()
        {
            var page = pages[pageIndex];
            page.SetActive(true);
        }

        public void ChangeForward()
        {
            ClosePage();
            pageIndex++;
            if(pageIndex >= pages.Count)
                pageIndex = 0;
            OpenPage();
        }
        public void ChangeBackward()
        {
            ClosePage();
            pageIndex--;
            if(pageIndex < 0)
                pageIndex = pages.Count - 1;
            OpenPage();
  
        }
    }
}
