CREATE DATABASE IF NOT EXISTS `test_addresses` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `test_addresses`;

--
-- Table structure for table `postal_code`
--
DROP TABLE IF EXISTS `postal_code`;
CREATE TABLE IF NOT EXISTS `postal_code` (
  `cPostalCode` char(4) NOT NULL,
  `cTownName` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`cPostalCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


