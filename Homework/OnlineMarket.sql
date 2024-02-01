--Continuing...



create table tags(id int,
				  tag_name varchar(255),
				  icon text,
				  created_at timestamptz,
				  uploated_at timestamptz,
				 created_by UUID,
				 updated_by UUID);

create table categories(id UUID,
					   parent_id UUID,
					   category_name varchar(255),
					   category_description text,
					   icon text,
					   image_path text,
					   active boolean,
					   created_at timestamptz,
				  	   uploated_at timestamptz,
				 		created_by UUID,
				 		updated_by UUID);
						
create table staff_accounts(id UUID,
					 first_name varchar(100),
					 last_name varchar(100),
					 phone_number varchar(100),
					 email varchar(255),
					 password_hash text,
					 active boolean,
					 profile_img text,
					 registered_at timestamptz,
					 uploadet_at timestamptz,
					 created_by UUID,
					 uploaded_by UUID);

