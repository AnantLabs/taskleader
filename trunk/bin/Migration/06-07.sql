/* Mise à jour de la table Contextes */

	CREATE TEMPORARY TABLE Contextes_backup(id,Titre);
	INSERT INTO Contextes_backup SELECT rowid,Titre FROM Contextes;
	DROP TABLE Contextes;
	CREATE TABLE [Contextes]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [Titre] VARCHAR (100) NOT NULL UNIQUE, [Defaut] BOOLEAN);
	INSERT INTO Contextes (id,Titre) SELECT * FROM Contextes_backup;
	DROP TABLE Contextes_backup;

/* Mise à jour de la table Sujets */

	CREATE TEMPORARY TABLE Sujets_backup(id,CtxtID,Titre);
	INSERT INTO Sujets_backup SELECT rowid,CtxtID,Titre FROM Sujets;
	DROP TABLE Sujets;
	CREATE TABLE [Sujets]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [CtxtID] REFERENCES [Contextes]([id]), [Titre] VARCHAR (100) NOT NULL, [Defaut] BOOLEAN);
	INSERT INTO Sujets (id,CtxtID,Titre) SELECT * FROM Sujets_backup;
	DROP TABLE Sujets_backup;

/* Mise à jour de la vueSujets */

	DROP VIEW VueSujets;

	CREATE VIEW VueSujets AS
	SELECT S.id, C.Titre as 'Contexte', S.Titre
	FROM Sujets S, Contextes C
	WHERE S.CtxtID = C.id;

/* Mise à jour de la table Destinataires */

	CREATE TEMPORARY TABLE Destinataires_backup(id,Nom);
	INSERT INTO Destinataires_backup SELECT rowid,Nom FROM Destinataires;
	DROP TABLE Destinataires;
	CREATE TABLE [Destinataires]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [Titre] VARCHAR (100) NOT NULL UNIQUE, [Defaut] BOOLEAN);
	INSERT INTO Destinataires (id,Titre) SELECT * FROM Destinataires_backup;
	DROP TABLE Destinataires_backup;

/* Mise à jour de la table Statuts */

	UPDATE Statuts SET Defaut=NULL WHERE Defaut='0'; 

	CREATE TEMPORARY TABLE Statuts_backup(id,Titre,Defaut);
	INSERT INTO Statuts_backup SELECT rowid,Titre,Defaut FROM Statuts;
	DROP TABLE Statuts;
	CREATE TABLE [Statuts]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [Titre] VARCHAR (100) NOT NULL UNIQUE, [Defaut] BOOLEAN);
	INSERT INTO Statuts SELECT * FROM Statuts_backup;
	DROP TABLE Statuts_backup;

/* Mise à jour de la table Filtres */

	CREATE TEMPORARY TABLE Filtres_backup(id,Titre,AllCtxt,AllSuj,AllDest,AllStat);
	INSERT INTO Filtres_backup SELECT rowid,Titre,AllCtxt,AllSuj,AllDest,AllStat FROM Filtres;
	DROP TABLE Filtres;
	CREATE TABLE [Filtres]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [Titre] VARCHAR (100) NOT NULL, [AllCtxt] BOOLEAN DEFAULT(1), [AllSuj] BOOLEAN DEFAULT(1), [AllDest] BOOLEAN DEFAULT(1), [AllStat] BOOLEAN DEFAULT(1), [Defaut] BOOLEAN);
	INSERT INTO Filtres (id,Titre,AllCtxt,AllSuj,AllDest,AllStat) SELECT * FROM Filtres_backup;
	DROP TABLE Filtres_backup;

/* Simplification du stockage des filtres */

	-- Création de la table de synthèse des différents contenus
	CREATE TABLE [Filtres_cont]([FiltreID] REFERENCES [Filtres]([id]), [FiltreType] VARCHAR(50), [SelectedID] VARCHAR(5));

	-- Insertion des filtres de contexte et effacement de la table
	INSERT INTO Filtres_cont SELECT FiltreID,'Contextes',SelectedID FROM Filtres_Ctxt;
	DROP TABLE Filtres_Ctxt;

	-- Insertion des filtres de sujet et effacement de la table
	INSERT INTO Filtres_cont SELECT FiltreID,'Sujets',SelectedID FROM Filtres_Suj;
	DROP TABLE Filtres_Suj;

	-- Insertion des filtres de destinataire et effacement de la table
	INSERT INTO Filtres_cont SELECT FiltreID,'Destinataires',SelectedID FROM Filtres_Dest;
	DROP TABLE Filtres_Dest;

	-- Insertion des filtres de statut et effacement de la table
	INSERT INTO Filtres_cont SELECT FiltreID,'Statuts',SelectedID FROM Filtres_Stat;
	DROP TABLE Filtres_Stat;

/* Mise à jour de la table Actions */

	-- Sauvegardes des ID des mails
	CREATE TEMPORARY TABLE IDMailTemp (ActionID,MailID);
	INSERT INTO IDMailTemp SELECT rowid,IDMail FROM Actions WHERE IDMail IS NOT NULL;

	-- Mise à jour de la table
	CREATE TEMPORARY TABLE Actions_backup(id,CtxtID,SujtID,Titre,DueDate,DestID,StatID);
	INSERT INTO Actions_backup SELECT rowid,CtxtID,SujtID,Titre,DueDate,DestID,StatID FROM Actions;
	DROP TABLE Actions;
	CREATE TABLE [Actions]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [CtxtID] VARCHAR REFERENCES [Contextes]([id]),[SujtID] VARCHAR REFERENCES [Sujets]([id]),[Titre] TEXT(300) NOT NULL,[DueDate] TEXT ,[DestID] VARCHAR REFERENCES [Destinataires]([id]),[StatID] VARCHAR REFERENCES [Statuts]([id]));
	INSERT INTO Actions SELECT * FROM Actions_backup;
	DROP TABLE Actions_backup;

/* Gestion des pièces jointes */

	-- Création de la table Enclosures
	CREATE TABLE [Enclosures]([ActionID] REFERENCES [Contextes]([id]), [EncType] VARCHAR(50), [EncID] VARCHAR(5));

	-- Ajout des mails déjà enregistrés
	INSERT INTO Enclosures SELECT ActionID,'Mails',MailID FROM IDMailTemp;

	-- Suppression de la table temporaire d'ID des mails
	DROP TABLE IDMailTemp;

/* Mise à jour de la table Mails */

	-- Ajout des colonnes ID et Titre
	CREATE TEMPORARY TABLE Mails_backup(id,StoreID,EntryID,MessageID);
	INSERT INTO Mails_backup SELECT rowid,StoreID,EntryID,MessageID FROM Mails;
	DROP TABLE Mails;
	CREATE TABLE [Mails]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [Titre] VARCHAR, [StoreID] VARCHAR NOT NULL, [EntryID] VARCHAR NOT NULL, [MessageID] VARCHAR NOT NULL);
	INSERT INTO Mails(id,StoreID,EntryID,MessageID) SELECT * FROM Mails_backup;
	DROP TABLE Mails_backup;
	
	-- Insertion de nom de mails génériques pour les mails déjà ajoutés en base
	UPDATE Mails SET Titre='Mail#' || id; 

/* Mise à jour de la vueActions */

	DROP VIEW VueActions;
	
	CREATE VIEW VueActions AS
	SELECT
		A.rowid as 'id',
		C.Titre as 'Contexte',
		Su.Titre as 'Sujet',
		A.Titre,
		CASE count(En.rowid)
			WHEN 1 THEN En.EncType||'#'||En.EncID
			WHEN 0 THEN NULL
			ELSE count(En.rowid)
		END
		'Liens',
		strftime("%d-%m-%Y",A.DueDate) as 'Deadline',
		D.Titre as 'Destinataire',
		St.Titre as 'Statut'
    FROM Actions A
		LEFT OUTER JOIN Contextes C
			ON A.CtxtID = C.id
		LEFT OUTER JOIN Sujets Su
			ON  A.SujtID = Su.id
		LEFT OUTER JOIN Enclosures En
			ON  A.id = En.ActionID
		LEFT OUTER JOIN Destinataires D
			ON  A.DestID = D.id
		LEFT OUTER JOIN Statuts St
			ON  A.StatID = St.id
	GROUP BY A.id
	ORDER BY DueDate ASC;
	
/* Création de la table Links */
	CREATE TABLE [Links]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [Titre] VARCHAR NOT NULL, [Path] VARCHAR NOT NULL);

/* Mise à jour de la table VerComp */

	-- Suppression de l'ancienne table
	DROP TABLE VerComp;
	
	-- Création de la nouvelle table
	CREATE TABLE [Properties]([Cle] VARCHAR,[Valeur] VARCHAR);
	
	-- Insertion de la version de la base Actions
	INSERT INTO Properties VALUES('ActionsDBVer','0.7');
