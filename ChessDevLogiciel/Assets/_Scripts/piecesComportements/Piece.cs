using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent((typeof(Outline)))]

[System.Serializable]
public abstract class Piece : MonoBehaviour
{
    protected Rigidbody _rb;
    protected MeshRenderer _meshRenderer;
    protected BoxCollider _boxCollider;
    protected Outline _outline;

    [SerializeField] protected Sprite _imagePiece;
    
    /// <summary>
    /// Si la pièce est activée et peut être seléctionnée.
    /// Si la pièce a été mangée, elle ne peut pas s'activer
    /// </summary>
    /// <returns></returns>
    private bool _estActive;

    private bool _estSelectionne;
    
    /// <summary>
    /// Returne si la pièce est déjà seléctionnée.
    /// </summary>
    /// <returns>Si la pièce est seléctionnée</returns>
    public bool EstSelectionne {
        protected set
        {
            _estSelectionne = value;

            if (value)
            { 
                _outline.enabled = true;  
            }
            else
            {
                _outline.enabled = false;
            }
        }
        get
        {
            return _estSelectionne;
        } 
    }
    
    /**
     * Joueur propriétaire de la pièce.
     * Est utilisé pour éviter les activation des cases qui contient une pièce su même joueur.
     */
    public Joueur.NumeroJoueur JoueurProprietaire;
    
    /// <summary>
    /// Ensemble de mouvement rélatives à la pièce.
    /// </summary>
    [HideInInspector] public Vector2Int[] moveSet;
    
    [HideInInspector] public Case caseActuelle;
    
    //Méthodes qui doivent être redéfiniées, car c'est un comportement propre à la pièce.
    
    /// <summary>
    /// Déplace la pièce.
    /// Selon le type de la pièce, celle-ci va se déplacer d'un façon ou une autre.
    /// </summary>
    /// <param name="caseDestination">Case de destination de la pièce</param>
    public abstract void DeplacerPiece(Case caseDestination);
    
    /// <summary>
    /// Seléctionne la pièce et affiche les possibilités du mouvement.
    /// Chaque pièce va afficher des possibilités de mouvement différentes.
    /// </summary>
    public abstract void SelectionnerPiece();
    
    /// <summary>
    /// Seléctionne la pièce et affiche les possibilités du mouvement.
    /// Chaque pièce va afficher des possibilités de mouvement différentes.
    /// </summary>
    public abstract void DeselectionnerPiece();

    private void Update()
    {
        //Pour voir le rayon utilisé pour trouver la case en dessous
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 2, Color.yellow);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Chaque fois qu'on entre dans une case, on sauvegarde la référence.
        if (other.CompareTag("CaseDuTableau"))
        {
            if (caseActuelle != null)
            {
               caseActuelle.SetPieceDansLaCase(null); //On efface la référence de la case 
            }

            caseActuelle = other.GetComponent<Case>(); //On s'ajoute au nouvelle case
            caseActuelle.SetPieceDansLaCase(this);
            
            //Debug.Log("Collision: " + caseActuelle.numeroDeCase);
        }
    }

    public void SetEstActive(bool estActive)
    {
        /*
        if (EstMangee)
        {
            _estActive = false;
        }
        else
        {
            _estActive = estActive;
        }*/

        _estActive = estActive;
    }

    public bool GetEstActive()
    {
        return _estActive;
    }

    public void CacherPiece()
    {
        _meshRenderer.enabled = false;
        _boxCollider.enabled = false;
    }

    public void MontrerPiece()
    {
        _meshRenderer.enabled = true;
        _boxCollider.enabled = true;
    }

    public Sprite GetPieceSprite()
    {
        return _imagePiece;
    }
}
