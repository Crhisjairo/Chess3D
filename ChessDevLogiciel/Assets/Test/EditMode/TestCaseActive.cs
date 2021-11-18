using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Test.EditMode
{
    public class TestCaseActive
    {
        private bool _PieceSelectionne;
        private bool _CaseActive;
        [Test]
        public void CaseEstActive()
        {
            _PieceSelectionne = true;
            
            if (_PieceSelectionne == true)
            {
                _CaseActive = true;
            }
            Assert.AreEqual(true,_CaseActive);
        }

        [Test]
        public void CaseEstPasActive()
        {
            _PieceSelectionne = false;

            if (_PieceSelectionne == false)
            { 
                _CaseActive = false;
            }
            Assert.AreEqual(false,_CaseActive);
        }




    }
}   