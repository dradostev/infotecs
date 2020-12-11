create table articles
(
    id bigserial not null
        constraint articles_pk
            primary key,
    username varchar(255) not null,
    title varchar(255) not null,
    content text not null,
    thumbnail bytea not null
);

create unique index articles_id_uindex 
    on articles (id);

create table comments
(
    id bigserial not null
        constraint comments_pk
            primary key,
    article_id bigint not null
        constraint comments_articles_id_fk
            references articles
            on delete cascade,
    username varchar(255) not null,
    content text not null
);

create unique index comments_id_uindex
	on comments (id);