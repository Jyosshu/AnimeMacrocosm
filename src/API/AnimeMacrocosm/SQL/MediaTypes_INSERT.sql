BEGIN TRANSACTION;


INSERT INTO MediaTypes (MediaTypeName,Resolution) VALUES ('Betamax','333x480');
INSERT INTO MediaTypes (MediaTypeName,Resolution) VALUES ('VHS','320×480');
INSERT INTO MediaTypes (MediaTypeName,Resolution) VALUES ('LaserDisc','560×480');
INSERT INTO MediaTypes (MediaTypeName,Resolution) VALUES ('VCD','352x240');
INSERT INTO MediaTypes (MediaTypeName,Resolution) VALUES ('SVCD','480×480');
INSERT INTO MediaTypes (MediaTypeName,Resolution) VALUES ('DVD','720×480');
INSERT INTO MediaTypes (MediaTypeName,Resolution) VALUES ('Blu-ray','1920×1080');
INSERT INTO MediaTypes (MediaTypeName,Resolution) VALUES ('Blu-ray 4k','3840×2160');
INSERT INTO MediaTypes (MediaTypeName,Resolution) VALUES ('VoD','Varied');


ROLLBACK;


COMMIT;