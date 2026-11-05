USE k4j_database;
CREATE TABLE IF NOT EXISTS tbl_employee_profile (
  user_id INT NOT NULL PRIMARY KEY,
  email VARCHAR(254) NOT NULL UNIQUE,
  middle_name VARCHAR(50) NULL,
  name_extension VARCHAR(12) NULL,
  date_of_birth DATE NOT NULL,
  gender VARCHAR(40) NULL,
  emergency_contact_person VARCHAR(120) NULL,
  emergency_contact_number VARCHAR(25) NULL,
  emergency_relationship VARCHAR(50) NULL,
  CONSTRAINT fk_employee_profile_user FOREIGN KEY (user_id) REFERENCES tbl_user(user_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
