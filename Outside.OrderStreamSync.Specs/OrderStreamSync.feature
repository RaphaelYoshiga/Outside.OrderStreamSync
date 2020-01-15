Feature: OrderStreamSync
	In order to ensure the backoffice system is up to date
	As a operations person
	I want website orders to be persisted in the backoffice

Scenario: Sync simple order
	Given we have this sales channel id 1 for AndroidApp
	When A new order message arrives into the message queue
	| OrderId | CustomerEmail | OrderAmount | SalesChannel |
	| 9999    | test@test.com | 157.53      | AndroidApp   |
	Then this order should have been persisted in the database
	| OrderId | CustomerEmail | OrderAmount | SalesChannelId |
	| 9999    | test@test.com | 157.53      | 1              |
