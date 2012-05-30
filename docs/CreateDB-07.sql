-- Création des tables
CREATE TABLE [Contextes]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [Titre] VARCHAR (100) NOT NULL UNIQUE, [Defaut] BOOLEAN);

CREATE TABLE [Sujets]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [CtxtID] REFERENCES [Contextes]([id]), [Titre] VARCHAR (100) NOT NULL, [Defaut] BOOLEAN);

CREATE TABLE [Destinataires]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [Titre] VARCHAR (100) NOT NULL UNIQUE, [Defaut] BOOLEAN);

CREATE TABLE [Statuts]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [Titre] VARCHAR (100) NOT NULL UNIQUE, [Defaut] BOOLEAN);
INSERT INTO Statuts(Titre,Defaut) VALUES('Ouverte',1);
INSERT INTO Statuts(Titre,Defaut) VALUES('Annulée',0);
INSERT INTO Statuts(Titre,Defaut) VALUES('Fermée',0);
INSERT INTO Statuts(Titre,Defaut) VALUES('Suspendue',0);

CREATE TABLE [Actions]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [CtxtID] VARCHAR REFERENCES [Contextes]([id]),[SujtID] VARCHAR REFERENCES [Sujets]([id]),[Titre] TEXT(300) NOT NULL,[DueDate] TEXT ,[DestID] VARCHAR REFERENCES [Destinataires]([id]),[StatID] VARCHAR REFERENCES [Statuts]([id]));

CREATE TABLE [Enclosures]([ActionID] REFERENCES [Actions]([id]), [EncType] VARCHAR(50), [EncID] VARCHAR(5));

CREATE TABLE [Filtres]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [Titre] VARCHAR (100) NOT NULL, [AllCtxt] BOOLEAN DEFAULT(1), [AllSuj] BOOLEAN DEFAULT(1), [AllDest] BOOLEAN DEFAULT(1), [AllStat] BOOLEAN DEFAULT(1), [Defaut] BOOLEAN);

CREATE TABLE [Filtres_cont]([FiltreID] REFERENCES [Filtres]([id]), [FiltreType] VARCHAR(50), [SelectedID] VARCHAR(5));

CREATE TABLE [Links]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [Titre] VARCHAR, [Path] VARCHAR NOT NULL);

CREATE TABLE [Mails]([id] INTEGER PRIMARY KEY AUTOINCREMENT, [Titre] VARCHAR, [StoreID] VARCHAR NOT NULL, [EntryID] VARCHAR NOT NULL, [MessageID] VARCHAR NOT NULL);

CREATE TABLE [Properties]([Cle] VARCHAR,[Valeur] VARCHAR);
INSERT INTO Properties VALUES('ActionsDBVer','0.7');

-- Création des vues

CREATE VIEW VueActions AS
	SELECT
		A.rowid as 'id',
		C.Titre as 'Contexte',
		Su.Titre as 'Sujet',
		A.Titre,
		count(En.rowid) as 'Liens',
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

CREATE VIEW VueSujets AS
	SELECT S.id, C.Titre as 'Contexte', S.Titre
	FROM Sujets S, Contextes C
	WHERE S.CtxtID = C.id;
	



