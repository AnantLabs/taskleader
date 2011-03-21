 /* 1. Suppression des valeurs de la colonne IDMail */

UPDATE Actions SET IDMail=NULL WHERE IDMail IS NOT NULL; 

/* 2. Création de la table Mails */

CREATE TABLE [Mails] ([StoreID] VARCHAR NOT NULL, [EntryID] VARCHAR NOT NULL, [MessageID] VARCHAR NOT NULL);

/* 3. Mise à jour de la vueActions */
/* En fait çà n'est pas nécessaire vu que l'IDMAil était déjà récupéré*/
DROP VIEW VueActions;

CREATE VIEW VueActions AS
   SELECT A.rowid as 'id', C.Titre as 'Contexte', Su.Titre as 'Sujet', A.Titre, A.IDMail as 'Mail', strftime("%d-%m-%Y",A.DueDate) as 'Deadline', D.Nom as 'Destinataire', St.Titre as 'Statut'
   FROM Actions A
      LEFT OUTER JOIN Contextes C
         ON A.CtxtID = C.rowid
      LEFT OUTER JOIN Sujets Su
         ON  A.SujtID = Su.rowid
      LEFT OUTER JOIN Destinataires D
         ON  A.DestID = D.rowid
      LEFT OUTER JOIN Statuts St
         ON  A.StatID = St.rowid
   ORDER BY DueDate ASC;

/* 4. Mise à jour de la table VerComp */

-- Supprimer toutes les entrées (pas de rétrocompatibilité)
DELETE FROM VerComp;
-- Rajouter la compatibilité 0.6
INSERT INTO VerComp VALUES ('0.6');