 /* 1. Suppression des valeurs de la colonne IDMail */

UPDATE Actions SET IDMail=NULL WHERE IDMail IS NOT NULL; 

/* 2. Création de la table Mails */

CREATE TABLE [Mails] ([StoreID] VARCHAR NOT NULL, [EntryID] VARCHAR NOT NULL, [MessageID] VARCHAR NOT NULL);

/* 3. Suppression de vueActions. Etant fortement liée à l'IHM, la vue sera créée par l'appli */

DROP VIEW VueActions;

/* 4. Mise à jour de la table VerComp */

-- Supprimer toutes les entrées (pas de rétrocompatibilité)
DELETE FROM VerComp;
-- Rajouter la compatibilité 0.6
INSERT INTO VerComp VALUES ('0.6');