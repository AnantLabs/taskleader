using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskLeader.DAL
{
    public delegate void ParentValueEventHandler(String parentValue);
    public delegate void ActionEditedEventHandler(String dbName, String actionID);

    /// <summary>
    /// Enumeration of possible fields
    /// </summary>
    public enum DBField // Liaison de 2 champs en ajoutant 100 à l'enfant
    {
        contexte = 1,
        sujet = 101, // Ce champ est lié au champ avec les mêmes 2 derniers digits
        destinataire = 2,
        statut = 3
    }

    public abstract class DB
    {
        public String name;

        #region Read methods

        /// <summary>
        /// Vérification de la présence d'une nouvelle valeur d'une entité
        /// </summary>
        /// <param name="field">DBField du champ à tester</param>
        /// <param name="title">Valeur du champ à tester</param>
        /// <returns></returns>
        public abstract bool isNvo(DBField field, String title);

        /// <summary>
        /// Vérification de la présence d'un nouveau sujet
        /// </summary>
        /// <param name="context"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        public abstract bool isNvoSujet(String context, String subject);

        // Récupération de la liste des valeurs d'une entité. Obsolète
        public abstract String[] getTitres(DBField field, String key = null);

        #endregion

        #region Write methods
        #endregion

        #region Events

        // Gestion des évènements NewValue - http://msdn.microsoft.com/en-us/library/z4ka55h8(v=vs.80).aspx
        protected Dictionary<String, Delegate> NewValue = new Dictionary<String, Delegate>();
        public void subscribe_NewValue(DBField entity, ParentValueEventHandler value) { this.NewValue[entity.nom] = (ParentValueEventHandler)this.NewValue[entity.nom] + value; }
        public void unsubscribe_NewValue(DBField entity, ParentValueEventHandler value) { this.NewValue[entity.nom] = (ParentValueEventHandler)this.NewValue[entity.nom] - value; }
        /// <summary>
        /// Génération de l'évènement NewValue
        /// </summary>
        /// <param name="entity">DBentity concernée</param>
        /// <param name="parentValue">La valeur courante de la DBentity parente</param>
        protected void OnNewValue(DBField entity, String parentValue = null)
        {
            ParentValueEventHandler handler;
            if (null != (handler = (ParentValueEventHandler)this.NewValue[entity.nom]))
                handler(parentValue);
        }

        // Gestion de l'évènement ActionEdited
        public event ActionEditedEventHandler ActionEdited;
        /// <summary>
        /// Génération de l'évènement ActionEdited
        /// </summary>
        /// <param name="action">Action ayant généré l'event</param>
        private void OnActionEdited(String actionID)
        {
            if (this.ActionEdited != null)
                this.ActionEdited(this.name, actionID); //Invoque le délégué
        }

        #endregion
    }
}
