/* 1. Migration de la table Sujets */

-- Renommage de l'ancienne table 
ALTER TABLE Sujets RENAME TO SujetsOld;

-- Création de la nouvelle table avec les colonnes renommées
CREATE TABLE [Sujets] ([CtxtID] REFERENCES [Contextes] ([rowid]), [Titre] VARCHAR (100) NOT NULL, [Descriptif] VARCHAR (200));

-- Copie des données 
INSERT INTO Sujets
SELECT C.rowid, Su.Titre, Su.Descriptif
FROM Contextes C, SujetsOld Su
WHERE Su.Contexte = C.Titre;

-- Suppression de l'ancienne table
DROP TABLE SujetsOld;

/* 2. Migration de la table Actions */

-- Renommage de l'ancienne table 
ALTER TABLE Actions RENAME TO ActionsOld;

-- Création de la nouvelle table avec les colonnes renommées
CREATE TABLE [Actions] ([CtxtID] VARCHAR REFERENCES [Contextes] ([rowid]), [SujtID] VARCHAR REFERENCES [Sujets] ([rowid]), [Titre] TEXT (300) NOT NULL, [DueDate] TEXT, [DestID] VARCHAR REFERENCES [Destinataires] ([rowid]), [IDMail] VARCHAR, [StatID] VARCHAR REFERENCES [Statuts] ([rowid]));

-- Copie des données
INSERT INTO Actions
SELECT C.rowid, Su.rowid, A.Titre, A.DueDate, D.rowid, NULL, St.rowid
FROM ActionsOld A, Contextes C, Sujets Su, Destinataires D, Statuts St
WHERE A.Contexte = C.Titre
AND  A.Sujet = Su.Titre
AND A.Destinataire = D.Nom
AND A.Statut = St.Titre;

-- Suppression de l'ancienne table
DROP TABLE ActionsOld;

/* 3. Créer la vue "jointure" des actions */

CREATE VIEW VueActions AS
   SELECT A.rowid as 'id', C.Titre as 'Contexte', Su.Titre as 'Sujet', A.Titre, strftime("%d-%m-%Y",A.DueDate) as 'Deadline', D.Nom as 'Destinataire', A.IDMail, St.Titre as 'Statut'
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
 
/* 4. Création de la vue "jointure" des sujets */

CREATE VIEW VueSujets AS
SELECT S.rowid as 'id', C.Titre as 'Contexte', S.Titre, S.Descriptif
FROM Sujets S, Contextes C
WHERE S.CtxtID = C.rowid;

/*5. Création des tables Filtres */

CREATE TABLE [Filtres] ([Titre] TEXT (300) NOT NULL, [AllCtxt] BOOLEAN DEFAULT(1), [AllSuj] BOOLEAN DEFAULT(1), [AllDest] BOOLEAN DEFAULT(1), [AllStat] BOOLEAN DEFAULT(1));

CREATE TABLE [Filtres_Ctxt] ([FiltreID] VARCHAR REFERENCES [Filtres] ([rowid]), [SelectedID] VARCHAR REFERENCES [Contextes] ([rowid]));

CREATE TABLE [Filtres_Suj] ([FiltreID] VARCHAR REFERENCES [Filtres] ([rowid]),[SelectedID] VARCHAR REFERENCES [Sujets] ([rowid]));

CREATE TABLE [Filtres_Dest] ([FiltreID] VARCHAR REFERENCES [Filtres] ([rowid]), [SelectedID] VARCHAR REFERENCES [Destinataires] ([rowid]));

CREATE TABLE [Filtres_Stat] ([FiltreID] VARCHAR REFERENCES [Filtres] ([rowid]), [SelectedID] VARCHAR REFERENCES [Statuts] ([rowid]));

/* 6. Modification de la table Statuts */

ALTER TABLE Statuts ADD COLUMN [Defaut] INT DEFAULT(0);
UPDATE Statuts SET Defaut='1' WHERE Titre='Ouverte';

/* 7. Création de la table Mails */

CREATE TABLE [Mails] ([StoreID] VARCHAR NOT NULL, [EntryID] VARCHAR NOT NULL, [MessageID] VARCHAR NOT NULL); 

/* 8. Mise à jour de la table VerComp */

-- Supprimer toutes les entrées
DELETE FROM VerComp;
-- Rajouter la compatibilité 0.5.0.0
INSERT INTO VerComp VALUES ('0.6');