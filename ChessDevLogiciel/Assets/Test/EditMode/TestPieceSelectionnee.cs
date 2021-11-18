using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;


namespace Test.EditMode
{
    public class TestPieceSelectionnee
    {
        private bool _EstSelectionnee = false;
        private int _cliquer = 1;
        
        // A Test behaves as an ordinary method
        [Test]
        public void EstSelectionneVrai()
        {
            _cliquer = 1;
            

            if(_cliquer == 1)
            {
                _EstSelectionnee = true;
            }
            else if (_cliquer == 2)
            {
                _EstSelectionnee = false;
            }
            Assert.AreEqual(true, _EstSelectionnee);
        }
        
        [Test]
        public void EstSelectionneFaux()
        {
            _cliquer = 2;
            if (_cliquer == 1)
            {
                _EstSelectionnee = true;
            }
            else if (_cliquer == 2)
            {
                _EstSelectionnee = false;
            }
            
            Assert.AreEqual(false,_EstSelectionnee);

        }

    
    }  
}